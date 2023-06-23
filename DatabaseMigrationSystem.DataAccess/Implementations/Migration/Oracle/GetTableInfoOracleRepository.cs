using Dapper;
using DatabaseMigrationSystem.DataAccess.Entity;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using Oracle.ManagedDataAccess.Client;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Migration.Oracle;

public class GetTableInfoOracleRepository: IGetTableInfoRepository
{
    private readonly string _connectionString;

    public GetTableInfoOracleRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public async Task<List<TableInfo>> Get(CancellationToken cancellationToken)
    {
        await using var connection = new OracleConnection(_connectionString);
        
        const string query = @"
            SELECT DISTINCT
            ut.table_name AS TableName,
            ut.owner AS Schema,
            ut.num_rows AS RowCount,
            CASE
                WHEN uc.constraint_type = 'P' THEN 1
                ELSE 0
            END AS HasParent,
            (
                SELECT uc2.table_name
                FROM all_constraints uc2
                WHERE uc2.constraint_name = uc.r_constraint_name AND uc2.owner = uc.r_owner
            ) AS ParentTableName
        FROM
            user_tables ut
            LEFT JOIN all_constraints uc
            ON ut.table_name = uc.table_name AND ut.owner = uc.owner
            LEFT JOIN all_cons_columns ucc 
            ON uc.constraint_name = ucc.constraint_name AND uc.owner = ucc.owner
        WHERE
            ut.owner NOT IN ('SYS', 'SYSTEM')
            AND (uc.constraint_type IN ('P', 'R') OR uc.constraint_type IS NULL)
    ";

        var tableInfos = new Dictionary<string, TableInfo>();

        var results = await connection.QueryAsync(query);

        foreach (var result in results)
        {
            var tableName = result.TableName;
            var schema = result.Schema;
            var hasParent = result.HasParent;
            var rowCount = result.RowCount;
            var parentTableName = result.ParentTableName;

            var tableFullName = $"{schema}.{tableName}";

            if (!tableInfos.ContainsKey(tableFullName))
            {
                tableInfos[tableFullName] = new TableInfo { TableName = tableName, Schema = schema, RowCount = rowCount };
            }

            var tableInfo = tableInfos[tableFullName];

            if (hasParent && parentTableName != null)
            {
                var parentTableFullName = $"{schema}.{parentTableName}";

                if (!tableInfos.ContainsKey(parentTableFullName))
                {
                    tableInfos[parentTableFullName] = new TableInfo { TableName = parentTableName, Schema = schema };
                }

                tableInfo.HasParent = true;
                tableInfo.ParentTableName = parentTableName;

                var parentTableInfo = tableInfos[parentTableFullName];
                parentTableInfo.ChildTables.Add(tableName);
            }
        }

        return new List<TableInfo>(tableInfos.Values.OrderBy(ti => ti.TableName));
    }
}