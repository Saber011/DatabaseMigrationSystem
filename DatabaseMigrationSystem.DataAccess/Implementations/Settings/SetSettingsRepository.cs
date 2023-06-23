using DatabaseMigrationSystem.DataAccess.Interfaces.Settings;
using DatabaseMigrationSystem.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Settings;

public class SetSettingsRepository: ISetSettingsRepository
{
    private readonly Func<ApplicationDbContext> _contextFactory;

    public SetSettingsRepository(Func<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
    
    public async Task Mutate(Infrastructure.DbContext.Entities.Settings request, CancellationToken cancellationToken)
    {
        await using var context = _contextFactory();

        var existingRecord = await context.Settings.FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);
    
        if (existingRecord != null)
        {
            request.Id = existingRecord.Id;
            context.Entry(existingRecord).CurrentValues.SetValues(request);
        }
        else
        {
            await context.Settings.AddAsync(request, cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
