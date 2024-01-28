using DatabaseMigrationSystem.Common.Enums;
using DatabaseMigrationSystem.DataAccess.Implementations.Migration.MongoDb;
using DatabaseMigrationSystem.DataAccess.Implementations.Migration.MySQl;
using DatabaseMigrationSystem.DataAccess.Implementations.Migration.PostgresSql;
using DatabaseMigrationSystem.DataAccess.Implementations.Migration.SqlServer;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Migration;

public class GetTableInfoRepositoryFactory : IGetTableInfoRepositoryFactory
{
    public IGetTableInfoRepository Create(DatabaseType type, string connectionString)
    {
        return type switch
        {
            DatabaseType.Oracle => new GetTableInfoMySQlRepository(connectionString),
            DatabaseType.PostgresSql => new GetTableInfoPostgresSqlRepository(connectionString),
            DatabaseType.SqlServer => new GetTableInfoSqlServerRepository(connectionString),
            DatabaseType.MySQl => new GetTableInfoMySQlRepository(connectionString),
            DatabaseType.MongoDb => new GetTableInfoMongoDbRepository(connectionString),
            _ => throw new NotSupportedException($"Service of type {type} is not supported.")
        };
    }
}