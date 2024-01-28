using DatabaseMigrationSystem.Common.Enums;
using DatabaseMigrationSystem.DataAccess.Implementations.Migration.MongoDb;
using DatabaseMigrationSystem.DataAccess.Implementations.Migration.MySQl;
using DatabaseMigrationSystem.DataAccess.Implementations.Migration.Oracle;
using DatabaseMigrationSystem.DataAccess.Implementations.Migration.PostgresSql;
using DatabaseMigrationSystem.DataAccess.Implementations.Migration.SqlServer;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using DatabaseMigrationSystem.Infrastructure.DbContext;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Migration;

public class WriteDataRepositoryFactory : IWriteDataRepositoryFactory
{
    private readonly Func<ApplicationDbContext> _contextFactory;
    public WriteDataRepositoryFactory(Func<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
    
    public IWriteDataRepository Create(DatabaseType type, string connectionString, bool withBinarySerializable = false)
    {
        return type switch
        {
            DatabaseType.Oracle => new WriteDataOracleRepository(connectionString, _contextFactory),
            DatabaseType.PostgresSql => new WriteDataPostgresSqlRepository(connectionString, _contextFactory),
            DatabaseType.SqlServer => new WriteDataSqlServerRepository(connectionString, _contextFactory),
            DatabaseType.MySQl => new WriteDataMySQLRepository(connectionString, _contextFactory),
            DatabaseType.MongoDb => new WriteDataMongoDbRepository(connectionString, _contextFactory),
            _ => throw new NotSupportedException($"Service of type {type} is not supported.")
        };
    }
}