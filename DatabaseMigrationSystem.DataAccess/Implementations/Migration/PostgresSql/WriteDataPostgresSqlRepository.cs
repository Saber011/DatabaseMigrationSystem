using System.Collections.Concurrent;
using Dapper;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using Npgsql;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Migration.PostgresSql;

public class WriteDataPostgresSqlRepository : IWriteDataRepository
{
    private readonly string _connectionString;

    public WriteDataPostgresSqlRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public async Task WriteDataAsync(string schema, string table, BlockingCollection<IList<dynamic>> dataQueue)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        // Получаем информацию о столбцах таблицы
        var columns = await GetColumnsInfo(connection, schema, table);

        // Создаем экземпляр NpgsqlCopyIn
        await using var writer = await connection.BeginBinaryImportAsync($"COPY {schema}.{table} ({string.Join(",", columns.Select(c => c.ColumnName))}) FROM STDIN (FORMAT BINARY)");

        // В цикле обходим все партии данных из очереди
        foreach (var dataBatch in dataQueue.GetConsumingEnumerable())
        {
            // Конвертируем каждую строку данных из списка в массив байтов
            foreach (var dataRow in (dataBatch.Select(x => (IDictionary<string, object>)x)))
            {
                var rowValues = columns.Select(column => dataRow[char.ToUpperInvariant(column.ColumnName[0]) + column.ColumnName.Substring(1)] ?? DBNull.Value).ToArray();
                writer.WriteRow(rowValues);
            }
        }

        // Завершаем вставку
        await writer.CompleteAsync();
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
        case "character varying":
            return typeof(string);
        case "boolean":
            return typeof(bool);
        case "date":
            return typeof(DateTime);
        default:
            throw new NotSupportedException($"Unsupported SQL data type: {sqlType}");
    }
}
}
