using DatabaseMigrationSystem.Common.Enums;
using DatabaseMigrationSystem.DataAccess.Implementations.Migration.MySQl;
using DatabaseMigrationSystem.DataAccess.Implementations.Migration.Oracle;
using DatabaseMigrationSystem.DataAccess.Implementations.Migration.PostgresSql;
using DatabaseMigrationSystem.DataAccess.Implementations.Migration.SqlServer;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Migration;

public class ReadDataRepositoryFactory : IReadDataRepositoryFactory
{
    public ReadDataRepository Create(DatabaseType type, string connectionString)
    {
        return type switch
        {
            DatabaseType.Oracle => new ReadDataOracleRepository(connectionString),
            DatabaseType.PostgresSql => new ReadDataPostgresSqlRepository(connectionString),
            DatabaseType.SqlServer => new ReadDataSqlServerRepository(connectionString),
            DatabaseType.MySQl => new ReadDataMySqlRepository(connectionString),
            DatabaseType.MongoDb => new ReadDataMongoDbRepository(connectionString),
            _ => throw new NotSupportedException($"Service of type {type} is not supported.")
        };
    }
}