using Dapper;
using DatabaseMigrationSystem.DataAccess.Entity;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using Npgsql;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Migration.PostgresSql;

public class GetDataInfoRepository : IGetDataInfoRepository
{
    private readonly string _connectionString;

    public GetDataInfoRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public async Task<CollectionInfo[]> Get(CancellationToken cancellationToken)
    {
          await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var tables = await connection.QueryAsync<CollectionInfo>(@$"
SELECT
    relname AS {nameof(CollectionInfo.CollectionName)},
    n_live_tup AS {nameof(CollectionInfo.DocumentCount)}
FROM
    pg_stat_user_tables
WHERE
    schemaname NOT IN ('pg_catalog', 'information_schema')
ORDER BY
    schemaname, relname;");
        
        return tables.ToArray();
    }
}