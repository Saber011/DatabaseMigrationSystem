using DatabaseMigrationSystem.Common.Enums;
using DatabaseMigrationSystem.DataAccess.Implementations.Migration.MySQl;
using DatabaseMigrationSystem.DataAccess.Implementations.Migration.Oracle;
using DatabaseMigrationSystem.DataAccess.Implementations.Migration.PostgresSql;
using DatabaseMigrationSystem.DataAccess.Implementations.Migration.SqlServer;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Migration;

public class WriteDataRepositoryFactory : IWriteDataRepositoryFactory
{
    public IWriteDataRepository Create(DatabaseType type, string connectionString)
    {
        return type switch
        {
            DatabaseType.Oracle => new WriteDataOracleRepository(connectionString),
            DatabaseType.PostgresSql => new WriteDataPostgresSqlRepository(connectionString),
            DatabaseType.SqlServer => new WriteDataSqlServerRepository(connectionString),
            DatabaseType.MySQl => new WriteDataMySQLRepository(connectionString),
            _ => throw new NotSupportedException($"Service of type {type} is not supported.")
        };
    }
}