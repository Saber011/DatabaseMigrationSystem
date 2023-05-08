using System.Collections.Concurrent;
using Dapper;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using MySqlConnector;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Migration.MySQl;

public class WriteDataMySQLRepository : IWriteDataRepository
{
    private readonly string _connectionString;

    public WriteDataMySQLRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public async Task WriteDataAsync(string schema, string table, BlockingCollection<IList<dynamic>> dataQueue)
    {
        await using var destinationConnection = new MySqlConnection(_connectionString);
        await destinationConnection.OpenAsync();

        // Получаем информацию о столбцах таблицы
        var columns = await GetColumnsInfo(destinationConnection, schema, table);

        // Записываем данные в таблицу
        foreach (var dataBatch in dataQueue.GetConsumingEnumerable())
        {
            using var stream = ToCsvStream(dataBatch, columns);
            var bulkLoader = new MySqlBulkLoader(destinationConnection)
            {
                FieldTerminator = ",",
                LineTerminator = "\n",
                FieldQuotationCharacter = '"',
                EscapeCharacter = '"',
                FieldQuotationOptional = true,
                Local = true,
                TableName = $"{schema}.{table}",
                SourceStream = stream
            };

            await bulkLoader.LoadAsync();
        }
    }

private async Task<IList<(string ColumnName, Type ColumnType)>> GetColumnsInfo(MySqlConnection connection, string schema, string table)
{
    var columnsQuery = $@"SELECT COLUMN_NAME, DATA_TYPE
                          FROM INFORMATION_SCHEMA.COLUMNS
                          WHERE TABLE_SCHEMA = @Schema AND TABLE_NAME = @Table;";

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
        case "int":
        case "smallint":
        case "tinyint":
        case "mediumint":
            return typeof(int);
        case "bigint":
            return typeof(long);
        case "decimal":
        case "float":
        case "double":
            return typeof(decimal);
        case "varchar":
        case "char":
        case "text":
        case "mediumtext":
        case "longtext":
            return typeof(string);
        case "date":
        case "datetime":
        case "timestamp":
            return typeof(DateTime);
        // Добавьте дополнительные соответствия для других типов данных
        default:
            throw new NotSupportedException($"Unsupported SQL data type: {sqlType}");
    }
}

    private MemoryStream ToCsvStream(IEnumerable<dynamic> data, IList<(string ColumnName, Type ColumnType)> columns)
    {
        var stream = new MemoryStream();
        var streamWriter = new StreamWriter(stream);

        foreach (var row in data)
        {
            var line = string.Join(",", columns.Select(column => QuoteValue(row[column.ColumnName])));
            streamWriter.WriteLine(line);
        }

        streamWriter.Flush();
        stream.Seek(0, SeekOrigin.Begin);

        return stream;
    }
    private string QuoteValue(object value)
    {
        if (value == null)
            return string.Empty;

        var stringValue = value.ToString();
        // Экранирование двойных кавычек внутри значения
        stringValue = stringValue.Replace("\"", "\"\"");

        // Если значение содержит запятую, перевод строки или двойные кавычки, оберните его в двойные кавычки
        if (stringValue.Contains(",") || stringValue.Contains("\"") || stringValue.Contains("\n"))
        {
            stringValue = $"\"{stringValue}\"";
        }

        return stringValue;
    }
}