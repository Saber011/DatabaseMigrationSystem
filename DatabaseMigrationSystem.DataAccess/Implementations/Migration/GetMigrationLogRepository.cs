using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using DatabaseMigrationSystem.Infrastructure.DbContext;
using DatabaseMigrationSystem.Infrastructure.DbContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Migration;

public class GetMigrationLogRepository : IGetMigrationLogRepository
{
    private readonly Func<ApplicationDbContext> _contextFactory;

    public GetMigrationLogRepository(Func<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
    
    public async Task<MigrationLog> Get(int request, CancellationToken cancellationToken)
    {
        await using var context = _contextFactory();

        return await context.MigrationLog.Where(x => x.UserId == request)
            .OrderByDescending(x => x.Date)
            .FirstOrDefaultAsync(cancellationToken);
    }
}