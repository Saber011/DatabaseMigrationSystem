using DatabaseMigrationSystem.ApplicationServices.Implementations.Migration.PostgresSql;
using DatabaseMigrationSystem.ApplicationServices.Implementations.Migration.SqlServer;
using DatabaseMigrationSystem.ApplicationServices.Interfaces.Migration;
using DatabaseMigrationSystem.Common.Enums;

namespace DatabaseMigrationSystem.ApplicationServices.Implementations.Migration;

public class MigrationServiceFactory : IMigrationServiceFactory
{
    public IMigrationService Create(DatabaseType type)
    {
        return type switch
        {
            DatabaseType.Oracle => new OracleMigrationService(),
            DatabaseType.PostgresSql => new PostgresSqlMigrationService(),
            DatabaseType.SqlServer => new SqlServerMigrationService(),
            DatabaseType.MySQl => new MySQlMigrationService(),
            _ => throw new NotSupportedException($"Service of type {type} is not supported.")
        };
    }
}