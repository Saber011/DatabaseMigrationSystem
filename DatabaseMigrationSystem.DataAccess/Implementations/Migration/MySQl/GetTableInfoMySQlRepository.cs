using Dapper;
using DatabaseMigrationSystem.DataAccess.Entity;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using MySqlConnector;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Migration.MySQl;

public class GetTableInfoMySQlRepository: IGetTableInfoRepository
{
    private readonly string _connectionString;

    public GetTableInfoMySQlRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public async Task<List<TableInfo>> Get(CancellationToken cancellationToken)
    {
        await using var connection = new MySqlConnection(_connectionString);
        var tableInfos = new List<TableInfo>();

        var tablesQuery = @"
                SELECT TABLE_SCHEMA, TABLE_NAME
                FROM information_schema.tables
                WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_SCHEMA = DATABASE();
            ";
        var tables = await connection.QueryAsync<(string TableSchema, string TableName)>(tablesQuery);


        var foreignKeysQuery = @"
                SELECT 
                    TABLE_SCHEMA, 
                    TABLE_NAME, 
                    COLUMN_NAME, 
                    REFERENCED_TABLE_SCHEMA, 
                    REFERENCED_TABLE_NAME, 
                    REFERENCED_COLUMN_NAME 
                FROM information_schema.key_column_usage
                WHERE REFERENCED_TABLE_SCHEMA = DATABASE();
            ";
        var foreignKeys = await connection.QueryAsync<ForeignKeyInfo>(foreignKeysQuery);

        foreach (var table in tables)
        {
            var tableInfo = new TableInfo
            {
                TableName = table.TableName,
                Schema = table.TableSchema
            };

            // Check if the table has a parent table
            var parentKey = foreignKeys.FirstOrDefault(fk =>
                fk.TableSchema == table.TableSchema &&
                fk.TableName == table.TableName &&
                fk.ReferencedColumnName == "id"
            );
            if (parentKey != null)
            {
                tableInfo.HasParent = true;
                tableInfo.ParentTableName = parentKey.ReferencedTableName;
            }

            // Get all child tables
            var childTables = foreignKeys
                .Where(fk =>
                    fk.ReferencedTableSchema == table.TableSchema &&
                    fk.ReferencedTableName == table.TableName
                )
                .Select(fk => $"{fk.TableSchema}.{fk.TableName}");
            tableInfo.ChildTables = childTables.ToList();

            tableInfos.Add(tableInfo);
        }

        return tableInfos;
    }
    
    private class ForeignKeyInfo
    {
        public string TableSchema { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string ReferencedTableSchema { get; set; }
        public string ReferencedTableName { get; set; }
        public string ReferencedColumnName { get; set; }
    }
}