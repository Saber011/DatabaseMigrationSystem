using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using DatabaseMigrationSystem.Infrastructure.DbContext;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Migration;

public class WriteMigrationLogRepository : IWriteMigrationLogRepository
{
    private readonly Func<ApplicationDbContext> _contextFactory;

    public WriteMigrationLogRepository(Func<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
    
    public async Task Mutate(Infrastructure.DbContext.Entities.MigrationLog request, CancellationToken cancellationToken)
    {
        await using var context = _contextFactory();

        await context.MigrationLog.AddAsync(request, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
    }
}