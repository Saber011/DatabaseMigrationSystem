using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using DatabaseMigrationSystem.Infrastructure.DbContext;
using DatabaseMigrationSystem.Infrastructure.DbContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Migration;

public class GetMigrationLogsRepository : IGetMigrationLogsRepository
{
    private readonly Func<ApplicationDbContext> _contextFactory;

    public GetMigrationLogsRepository(Func<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
    
    public async Task<MigrationLog[]> Get(Guid request, CancellationToken cancellationToken)
    {
        await using var context = _contextFactory();

        return await context.MigrationLog
            .Where(x => x.ImportSessionId == request)
            .OrderByDescending(x => x.Date)
            .ToArrayAsync(cancellationToken);
    }
}