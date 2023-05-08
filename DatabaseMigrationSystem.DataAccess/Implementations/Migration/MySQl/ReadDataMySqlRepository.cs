using System.Collections.Concurrent;
using Dapper;
using DatabaseMigrationSystem.DataAccess.Entity;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using MySqlConnector;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Migration.MySQl;

public class ReadDataMySqlRepository : ReadDataRepository
{
    private readonly string _connectionString;

    public ReadDataMySqlRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public override async Task ReadDataAsync(string schema, string table, BlockingCollection<IList<dynamic>> dataQueue)
    {
        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        var offset = 0;
        while (true)
        {
            var data = await connection.QueryAsync<dynamic>($@"
                    SELECT *
                    FROM {schema}.{table}
                    ORDER BY NULL
                    LIMIT {offset}, {BatchSize}");

            var dataList = data.ToList();
            if (dataList.Count > 0)
            {
                dataQueue.Add(dataList);
                offset += BatchSize;
            }
            else
            {
                break;
            }
        }

        dataQueue.CompleteAdding();
    }
}