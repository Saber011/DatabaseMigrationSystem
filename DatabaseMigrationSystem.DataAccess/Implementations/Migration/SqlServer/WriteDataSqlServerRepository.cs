using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using DatabaseMigrationSystem.Infrastructure.DbContext;
using DatabaseMigrationSystem.Infrastructure.DbContext.Entities;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Migration.SqlServer;

public class WriteDataSqlServerRepository : IWriteDataRepository
{
    private readonly string _connectionString;
    private readonly Func<ApplicationDbContext> _contextFactory;

    public WriteDataSqlServerRepository(string connectionString, Func<ApplicationDbContext> contextFactory)
    {
        _connectionString = connectionString;
        _contextFactory = contextFactory;
    }

    public async Task WriteDataAsync(string schema, string table, BlockingCollection<IList<dynamic>> dataQueue, MigrationLog migrationLog,  CancellationToken cancellationToken)
    {
        await using var destinationConnection = new SqlConnection(_connectionString);
        await destinationConnection.OpenAsync(cancellationToken);

        foreach (var dataBatch in dataQueue.GetConsumingEnumerable())
        {
            // Создаем DataTable для хранения данных пакета
            DataTable dataTable = new DataTable();

            // Заполняем DataTable данными из dataBatch
            foreach (var row in dataBatch)
            {
                DataRow dataRow = dataTable.NewRow();

                // Получаем имена столбцов для DapperRow
                var columnNames = ((IDictionary<string, object>)row).Keys;

                foreach (var columnName in columnNames)
                {
                    object value;
                    if (!((IDictionary<string, object>)row).TryGetValue(columnName, out value))
                    {
                        value = DBNull.Value;
                    }

                    if (dataTable.Columns.Contains(columnName))
                    {
                        dataRow[columnName] = value ?? DBNull.Value;
                    }
                    else
                    {
                        dataTable.Columns.Add(columnName, value?.GetType() ?? typeof(DBNull));
                        dataRow[columnName] = value ?? DBNull.Value;
                    }
                }

                dataTable.Rows.Add(dataRow);
            }

            // Используем SqlBulkCopy для массовой вставки данных в целевую таблицу
            using SqlBulkCopy bulkCopy = new SqlBulkCopy(destinationConnection);
            bulkCopy.DestinationTableName = $"{schema}.{table}";

            await bulkCopy.WriteToServerAsync(dataTable);
            
            migrationLog.DataCount = dataBatch.Count;
            migrationLog.Date = DateTime.UtcNow;
            await WriteLog(migrationLog);
        }
    }
    
    private async Task WriteLog(MigrationLog log)
    {
        var context = _contextFactory();

        context.MigrationLog.Add(log);

        await context.SaveChangesAsync();
    }
}