using DatabaseMigrationSystem.DataAccess.Interfaces.User;
using DatabaseMigrationSystem.Infrastructure.DbContext;
using DatabaseMigrationSystem.Infrastructure.DbContext.Entities;

namespace DatabaseMigrationSystem.DataAccess.Implementations.User;

public class CreateUserTokenRepository: ICreateUserTokenRepository
{
    private readonly Func<ApplicationDbContext> _contextFactory;

    public CreateUserTokenRepository(Func<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task Mutate(UserToken request, CancellationToken cancellationToken)
    {
        await using var context = _contextFactory();

        await context.UserTokens.AddAsync(request, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
    }
}