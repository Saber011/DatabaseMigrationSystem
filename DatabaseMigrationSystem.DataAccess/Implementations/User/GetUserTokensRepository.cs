using DatabaseMigrationSystem.DataAccess.Interfaces.User;
using DatabaseMigrationSystem.Infrastructure.DbContext;
using DatabaseMigrationSystem.Infrastructure.DbContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMigrationSystem.DataAccess.Implementations.User;

public class GetUserTokensRepository : IGetUserTokensRepository
{
    private readonly Func<ApplicationDbContext> _contextFactory;

    public GetUserTokensRepository(Func<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<UserToken[]> Get(int request, CancellationToken cancellationToken)
    {
        await using var context = _contextFactory();

        return await context.UserTokens.Where(x => x.UserId == request)
            .ToArrayAsync(cancellationToken);
    }
}