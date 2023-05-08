using DatabaseMigrationSystem.DataAccess.Interfaces.User;
using DatabaseMigrationSystem.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMigrationSystem.DataAccess.Implementations.User;

public class GetAllUsersRepository : IGetAllUsersRepository
{
    private readonly Func<ApplicationDbContext> _contextFactory;

    public GetAllUsersRepository(Func<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<Infrastructure.DbContext.Entities.User[]> Get(CancellationToken cancellationToken)
    {
        await using var context = _contextFactory();

        return await context.Users.ToArrayAsync(cancellationToken);
    }
}