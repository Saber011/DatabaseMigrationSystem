using System.Collections.Concurrent;
using Dapper;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using Oracle.ManagedDataAccess.Client;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Migration.Oracle;

public class ReadDataOracleRepository : ReadDataRepository
{
    private readonly string _connectionString;

    public ReadDataOracleRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public override async Task ReadDataAsync(string schema, string table, BlockingCollection<IList<dynamic>> dataQueue)
    {
        await using var connection = new OracleConnection(_connectionString);
        
        await connection.OpenAsync();

        var offset = 0;
        while (true)
        {
            var data = await connection.QueryAsync<dynamic>($@"
SELECT *
FROM (SELECT *
      FROM {schema}.{table}
      ORDER BY NULL)
WHERE ROWNUM >= {offset} + 1 AND ROWNUM <= {offset} + {BatchSize}");

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