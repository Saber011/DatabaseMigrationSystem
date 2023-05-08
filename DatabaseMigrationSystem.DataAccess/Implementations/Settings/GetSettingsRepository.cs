using DatabaseMigrationSystem.DataAccess.Interfaces.Settings;
using DatabaseMigrationSystem.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Settings;

public class GetSettingsRepository : IGetSettingsRepository
{
    private readonly Func<ApplicationDbContext> _contextFactory;

    public GetSettingsRepository(Func<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
    
    public async Task<Infrastructure.DbContext.Entities.Settings> Get(int request, CancellationToken cancellationToken)
    {
        await using var context = _contextFactory();

        return await context.Settings
            .FirstOrDefaultAsync(x => x.UserId == request, cancellationToken);
    }
}