using DatabaseMigrationSystem.Common.Enums;
using DatabaseMigrationSystem.DataAccess.Implementations.Migration.MongoDb;
using DatabaseMigrationSystem.DataAccess.Implementations.Migration.PostgresSql;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Migration;

public class GetDataInfoRepositoryFactory : IGetDataInfoRepositoryFactory
{
    public IGetDataInfoRepository Create(DatabaseType type, string connectionString)
    {
        return type switch
        {
            DatabaseType.MongoDb => new GetDataInfoMongoDbRepository(connectionString),
            DatabaseType.PostgresSql => new GetDataInfoRepository(connectionString),
            _ => throw new NotSupportedException($"Service of type {type} is not supported.")
        };
    }
}