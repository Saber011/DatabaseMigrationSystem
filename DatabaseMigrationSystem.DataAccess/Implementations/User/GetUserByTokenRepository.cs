using DatabaseMigrationSystem.DataAccess.Interfaces.User;
using DatabaseMigrationSystem.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMigrationSystem.DataAccess.Implementations.User;

public class GetUserByTokenRepository : IGetUserByTokenRepository
{
    private readonly Func<ApplicationDbContext> _contextFactory;

    public GetUserByTokenRepository(Func<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<Infrastructure.DbContext.Entities.User> Get(string request, CancellationToken cancellationToken)
    {
        await using var context = _contextFactory();

        var result = await context.UserTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == request, cancellationToken);

        return result?.User;
    }
}