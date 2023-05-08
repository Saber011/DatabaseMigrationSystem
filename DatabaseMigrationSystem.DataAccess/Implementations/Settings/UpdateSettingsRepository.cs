using DatabaseMigrationSystem.DataAccess.Interfaces.Settings;
using DatabaseMigrationSystem.Infrastructure.DbContext;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Settings;

public class UpdateSettingsRepository : IUpdateSettingsRepository
{
    private readonly Func<ApplicationDbContext> _contextFactory;

    public UpdateSettingsRepository(Func<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
    
    public async Task Mutate(Infrastructure.DbContext.Entities.Settings request, CancellationToken cancellationToken)
    {
        await using var context = _contextFactory();

        context.Settings.Update(request);

        await context.SaveChangesAsync(cancellationToken);
    }
}