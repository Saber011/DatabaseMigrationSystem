using System.Collections.Concurrent;
using System.Data;
using Dapper;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using DatabaseMigrationSystem.Infrastructure.DbContext;
using DatabaseMigrationSystem.Infrastructure.DbContext.Entities;
using Oracle.ManagedDataAccess.Client;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Migration.Oracle;

public class WriteDataOracleRepository : IWriteDataRepository
{
    private readonly string _connectionString;
    private readonly Func<ApplicationDbContext> _contextFactory;

    public WriteDataOracleRepository(string connectionString, Func<ApplicationDbContext> contextFactory)
    {
        _connectionString = connectionString;
        _contextFactory = contextFactory;
    }

    public async Task WriteDataAsync(string schema, string table, BlockingCollection<IList<dynamic>> dataQueue, MigrationLog migrationLog,  CancellationToken cancellationToken)
    {
        await using var destinationConnection = new OracleConnection(_connectionString);
        await destinationConnection.OpenAsync(cancellationToken);

        // Получаем информацию о столбцах таблицы
        var columns = await GetColumnsInfo(destinationConnection, schema, table);

        using var bulkCopy = new OracleBulkCopy(destinationConnection);
        bulkCopy.DestinationTableName = $"{schema}.{table}";

        // Создаем маппинг колонок
        foreach (var column in columns)
        {
            bulkCopy.ColumnMappings.Add(new OracleBulkCopyColumnMapping(column.ColumnName, column.ColumnName));
        }

        // Записываем данные в таблицу
        foreach (var dataBatch in dataQueue.GetConsumingEnumerable())
        {
            using var dataTable = ToDataTable(dataBatch, columns);
            bulkCopy.WriteToServer(dataTable);
            
            migrationLog.DataCount = dataBatch.Count;
            migrationLog.Date = DateTime.UtcNow;
            await WriteLog(migrationLog);
        }
    }

private async Task<IList<(string ColumnName, Type ColumnType)>> GetColumnsInfo(OracleConnection connection, string schema, string table)
{
    var columnsQuery = $@"SELECT COLUMN_NAME, DATA_TYPE
                          FROM ALL_TAB_COLUMNS
                          WHERE TABLE_NAME = :Table AND OWNER = :Schema";

    var columnsData = await connection.QueryAsync<(string ColumnName, string DataType)>(columnsQuery, new { Schema = schema, Table = table });

    var columns = columnsData.Select(columnData => (columnData.ColumnName, ColumnType: GetClrType(columnData.DataType))).ToList();
    return columns;
}

private Type GetClrType(string sqlType)
{
    // Метод для преобразования SQL типа данных в соответствующий CLR тип
    // Здесь необходимо определить соответствия между SQL и CLR типами в зависимости от вашего приложения
    switch (sqlType.ToLower())
    {
        case "number":
            return typeof(decimal);
        case "varchar2":
        case "nvarchar2":
        case "char":
        case "nchar":
            return typeof(string);
        case "date":
        case "timestamp":
            return typeof(DateTime);
        // Добавьте дополнительные соответствия для других типов данных
        default:
            throw new NotSupportedException($"Unsupported SQL data type: {sqlType}");
    }
}

private DataTable ToDataTable(IEnumerable<dynamic> data, IList<(string ColumnName, Type ColumnType)> columns)
{
    var dataTable = new DataTable();

    foreach (var column in columns)
    {
        dataTable.Columns.Add(column.ColumnName, column.ColumnType);
    }

    foreach (var row in data)
    {
        var dataRow = dataTable.NewRow();
        foreach (var column in columns)
        {
            dataRow[column.ColumnName] = row[column.ColumnName];
        }
        dataTable.Rows.Add(dataRow);
    }

    return dataTable;
}


private async Task WriteLog(MigrationLog log)
{
    var context = _contextFactory();

    context.MigrationLog.Add(log);

    await context.SaveChangesAsync();
}
}