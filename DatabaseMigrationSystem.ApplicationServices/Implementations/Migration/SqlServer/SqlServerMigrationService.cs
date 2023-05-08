using DatabaseMigrationSystem.ApplicationServices.Interfaces.Migration;

namespace DatabaseMigrationSystem.ApplicationServices.Implementations.Migration.SqlServer;

public class SqlServerMigrationService : IMigrationService
{
    public Task<object> Handle(string request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}