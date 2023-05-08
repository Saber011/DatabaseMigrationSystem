using System.Data.Common;
using System.Data.SqlClient;
using DatabaseMigrationSystem.Common.Enums;
using MySqlConnector;
using Npgsql;
using Oracle.ManagedDataAccess.Client;

namespace DatabaseMigrationSystem.Infrastructure.Validators;

public class ConnectionValidator : IConnectionValidator
{
    public async Task ValidateConnection(DatabaseType databaseType, string connectionString)
    {
        try
        {
            await using var connection = CreateDbConnection(databaseType, connectionString);
            await connection.OpenAsync();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private static DbConnection CreateDbConnection(DatabaseType databaseType, string connectionString)
    {
        return databaseType switch
        {
            DatabaseType.SqlServer => new SqlConnection(connectionString),
            DatabaseType.PostgresSql => new NpgsqlConnection(connectionString),
            DatabaseType.Oracle => new OracleConnection(connectionString),
            DatabaseType.MySQl => new MySqlConnection(connectionString),
            _ => throw new ArgumentException($"Unsupported database type: {databaseType}")
        };
    }
}