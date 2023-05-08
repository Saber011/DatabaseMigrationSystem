using System.Data.SqlClient;
using Dapper;
using DatabaseMigrationSystem.DataAccess.Entity;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Migration.SqlServer;

public class GetTableInfoSqlServerRepository: IGetTableInfoRepository
{
    private readonly string _connectionString;

    public GetTableInfoSqlServerRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public async Task<List<TableInfo>> Get(CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var tables = await connection.QueryAsync<TableInfo>(@"
                SELECT 
                    t.name as TableName,
                    s.name as ""Schema""
                FROM 
                    sys.tables t
                INNER JOIN 
                    sys.schemas s ON t.schema_id = s.schema_id
                ORDER BY 
                    t.name");

        var relations = await connection.QueryAsync<(string, string, string, string)>(@"
                SELECT
                    parent.name as ParentTableName,
                    child.name as ChildTableName,
                    s.name as ""Schema"",
                    fk.name as ForeignKeyName
                FROM
                    sys.foreign_keys fk
                INNER JOIN
                    sys.tables parent ON fk.referenced_object_id = parent.object_id
                INNER JOIN
                    sys.tables child ON fk.parent_object_id = child.object_id
                INNER JOIN
                    sys.schemas s ON parent.schema_id = s.schema_id");

        var tableInfoDict = tables.ToDictionary(
            t => (t.TableName, t.Schema),
            t => new TableInfo
            {
                TableName = t.TableName,
                Schema = t.Schema
            });

        foreach (var (parentTableName, childTableName, schema, _) in relations)
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