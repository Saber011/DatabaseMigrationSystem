using DatabaseMigrationSystem.DataAccess.Interfaces.User;
using DatabaseMigrationSystem.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMigrationSystem.DataAccess.Implementations.User;

public class GetByIdUserGetRepository : IGetByIdUserGetRepository
{
    private readonly Func<ApplicationDbContext> _contextFactory;

    public GetByIdUserGetRepository(Func<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<Infrastructure.DbContext.Entities.User> Get(int request, CancellationToken cancellationToken)
    {
        await using var context = _contextFactory();

        return await context.Users.FirstOrDefaultAsync(x => x.Id == request, cancellationToken);
    }
}