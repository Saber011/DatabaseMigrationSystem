using DatabaseMigrationSystem.DataAccess.Interfaces.User;
using DatabaseMigrationSystem.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMigrationSystem.DataAccess.Implementations.User;

public class GetByLoginUserRepository : IGetByLoginUserRepository
{
    private readonly Func<ApplicationDbContext> _contextFactory;

    public GetByLoginUserRepository(Func<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<Infrastructure.DbContext.Entities.User> Get(string request, CancellationToken cancellationToken)
    {
        await using var context = _contextFactory();

        return await context.Users.FirstOrDefaultAsync(x => x.Login == request, cancellationToken);
    }
}