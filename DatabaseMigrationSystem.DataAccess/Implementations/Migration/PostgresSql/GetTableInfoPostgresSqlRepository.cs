using System.Data;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using Npgsql;
using Dapper;
using DatabaseMigrationSystem.DataAccess.Entity;
using System.Linq;

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

        // Получение информации о таблицах
        var tables = await connection.QueryAsync<TableInfo>(@"
            SELECT 
            tc.table_name as TableName,
            tc.table_schema as Schema,
            pt.n_live_tup as RowCount
        FROM 
            information_schema.tables tc
        INNER JOIN 
            pg_stat_user_tables pt ON tc.table_name = pt.relname
        WHERE 
            tc.table_schema NOT IN ('pg_catalog', 'information_schema')
        ORDER BY 
            tc.table_name");

        // Получение информации о связях между таблицами
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

        // Получение информации о столбцах для каждой таблицы
        var columns = await connection.QueryAsync<(string TableName, string Schema, string ColumnName, string DataType)>(@"
            SELECT 
                table_name as TableName, 
                table_schema as Schema, 
                column_name as ColumnName, 
                data_type as DataType
            FROM 
                information_schema.columns
            WHERE 
                table_schema NOT IN ('pg_catalog', 'information_schema')
        ");

        var tableInfoDict = tables.ToDictionary(
            t => (t.TableName, t.Schema),
            t => new TableInfo
            {
                TableName = t.TableName,
                Schema = t.Schema,
                RowCount = t.RowCount,
                Columns = new List<ColumnInfo>() // Инициализация списка колонок
            });

        // Добавление информации о колонках в объекты TableInfo
        foreach (var column in columns)
        {
            if (tableInfoDict.TryGetValue((column.TableName, column.Schema), out var tableInfo))
            {
                tableInfo.Columns.Add(new ColumnInfo { ColumnName = column.ColumnName, DataType = column.DataType });
            }
        }

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
