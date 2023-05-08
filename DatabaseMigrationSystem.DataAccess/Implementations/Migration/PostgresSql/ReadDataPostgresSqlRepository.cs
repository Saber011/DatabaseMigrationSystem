using System.Collections.Concurrent;
using Dapper;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using Npgsql;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Migration.PostgresSql;

public class ReadDataPostgresSqlRepository : ReadDataRepository
{
    private readonly string _connectionString;

    public ReadDataPostgresSqlRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public override async Task ReadDataAsync(string schema, string table, BlockingCollection<IList<dynamic>> dataQueue)
    {
        
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        var offset = 0;
        while (true)
        {
            var data = await connection.QueryAsync<dynamic>($@"
SELECT *
FROM {schema}.{table}
ORDER BY (SELECT NULL)
OFFSET {offset} ROWS
FETCH NEXT {BatchSize} ROWS ONLY");

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