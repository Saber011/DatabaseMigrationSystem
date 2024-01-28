using System.Collections.Concurrent;
using Dapper;
using DatabaseMigrationSystem.Common.Enums;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using DatabaseMigrationSystem.Infrastructure.DbContext;
using DatabaseMigrationSystem.Infrastructure.DbContext.Entities;
using Npgsql;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Migration.PostgresSql;

public class WriteDataPostgresSqlRepository : IWriteDataRepository
{
    private readonly string _connectionString;
    private readonly Func<ApplicationDbContext> _contextFactory;

    public WriteDataPostgresSqlRepository(string connectionString, Func<ApplicationDbContext> contextFactory)
    {
        _connectionString = connectionString;
        _contextFactory = contextFactory;
    }
    
    public async Task WriteDataAsync(string schema, string table, BlockingCollection<IList<dynamic>> dataQueue, MigrationLog migrationLog,  CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        // Получаем информацию о столбцах таблицы
        var columns = await GetColumnsInfo(connection, schema, table);

        // Создаем экземпляр NpgsqlCopyIn
        var copyCommand =
            $"COPY {schema}.{table} ({string.Join(",", columns.Select(c => $"\"{c.ColumnName}\""))}) FROM STDIN (FORMAT BINARY)";
        await using var writer = await connection.BeginBinaryImportAsync(copyCommand);

        // В цикле обходим все партии данных из очереди
        foreach (var dataBatch in dataQueue.GetConsumingEnumerable())
        {
            // Конвертируем каждую строку данных из списка в массив байтов
            foreach (var dataRow in (dataBatch.Select(x => (IDictionary<string, object>)x)))
            {
                var rowValues = columns.Select(column => dataRow.FirstOrDefault(x => x.Key.ToLower() == column.ColumnName.ToLower()).Value ?? DBNull.Value).ToArray();
                await writer.WriteRowAsync(cancellationToken, rowValues);
            }

            var newLog = new MigrationLog
            {
                DataCount = dataBatch.Count,
                Date = DateTime.UtcNow,
                Schema = migrationLog.Schema,
                ImportSessionId = migrationLog.ImportSessionId,
                TableName = migrationLog.TableName,
                UserId = migrationLog.UserId,
                Status = MigrationStatus.Processed,
            };

            await WriteLog(newLog);
        }
        // Завершаем вставку
        await writer.CompleteAsync(cancellationToken);
    }
    
private async Task<IList<(string ColumnName, Type ColumnType)>> GetColumnsInfo(NpgsqlConnection connection, string schema, string table)
{
    var columnsQuery = $@"SELECT column_name, data_type
                          FROM information_schema.columns
                          WHERE table_schema = @Schema AND table_name = @Table;";

    var columnsData = await connection.QueryAsync<(string ColumnName, string DataType)>(columnsQuery, new { Schema = schema, Table = table });

    var columns = columnsData.Select(columnData => (columnData.ColumnName, ColumnType: GetClrType(columnData.DataType))).ToList();
    return columns;
}

private async Task WriteLog(MigrationLog log)
{
    var context = _contextFactory();

    context.MigrationLog.Add(log);

    await context.SaveChangesAsync();
}

private Type GetClrType(string sqlType)
{
    switch (sqlType.ToLower())
    {
        case "integer":
        case "int":
        case "smallint":
            return typeof(int);
        case "bigint":
            return typeof(long);
        case "numeric":
        case "decimal":
            return typeof(decimal);
        case "text":
        case "varchar":
        case  "char":
        case "character varying":
        case "character":
            return typeof(string);
        case "boolean":
            return typeof(bool);
        case "date":
            return typeof(DateTime);
        case "timestamp without time zone":
            return typeof(DateTime);
        default:
            throw new NotSupportedException($"Unsupported SQL data type: {sqlType}");
    }
}
}
