using System.Data;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using Npgsql;
using Dapper;
using DatabaseMigrationSystem.DataAccess.Entity;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Migration.PostgresSql;

public class GetTableInfoPostgresSqlRepository : IGetTableInfoRepository
{
    private readonly string _connectionString;

    public GetTableInfoPostgresSqlRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<TableInfo>> Get(CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var tables = await connection.QueryAsync<TableInfo>(@"
                SELECT 
                    tc.table_name as TableName,
                    tc.table_schema as Schema
                FROM 
                    information_schema.tables tc
                WHERE 
                    tc.table_schema NOT IN ('pg_catalog', 'information_schema')
                ORDER BY 
                    tc.table_name");

        var relations = await connection.QueryAsync<(string, string, string)>(@"
                SELECT
                    kcu1.table_name as ParentTableName,
                    kcu2.table_name as ChildTableName,
                    kcu2.constraint_schema as Schema
                FROM
                    information_schema.referential_constraints as rc
                INNER JOIN
                    information_schema.key_column_usage as kcu1
                    ON rc.constraint_name = kcu1.constraint_name
                    AND rc.constraint_schema = kcu1.constraint_schema
                INNER JOIN
                    information_schema.key_column_usage as kcu2
                    ON rc.unique_constraint_name = kcu2.constraint_name
                    AND rc.unique_constraint_schema = kcu2.constraint_schema
                WHERE
                    kcu1.constraint_schema NOT IN ('pg_catalog', 'information_schema')
                    AND kcu2.constraint_schema NOT IN ('pg_catalog', 'information_schema')");

        var tableInfoDict = tables.ToDictionary(
            t => (t.TableName, t.Schema),
            t => new TableInfo
            {
                TableName = t.TableName,
                Schema = t.Schema
            });

        foreach (var (parentTableName, childTableName, schema) in relations)
        {
            var parentTableInfo = tableInfoDict[(parentTableName, schema)];
            var childTableInfo = tableInfoDict[(childTableName, schema)];

            childTableInfo.HasParent = true;
            childTableInfo.ParentTableName = parentTableName;
            parentTableInfo.ChildTables.Add(childTableName);
        }

        return tableInfoDict.Values.ToList();
    }
}