using DatabaseMigrationSystem.DataAccess.Interfaces.User;
using DatabaseMigrationSystem.Infrastructure.DbContext;
using DatabaseMigrationSystem.Infrastructure.DbContext.Entities;

namespace DatabaseMigrationSystem.DataAccess.Implementations.User;

public class UpdateUserTokenRepository : IUpdateUserTokenRepository
{
    private readonly Func<ApplicationDbContext> _contextFactory;

    public UpdateUserTokenRepository(Func<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task Mutate(UserToken request, CancellationToken cancellationToken)
    {
        await using var context = _contextFactory();
        context.UserTokens.Update(request);

        await context.SaveChangesAsync(cancellationToken);
    }
}