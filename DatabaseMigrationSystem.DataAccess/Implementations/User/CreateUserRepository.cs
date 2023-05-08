using DatabaseMigrationSystem.DataAccess.Interfaces.User;
using DatabaseMigrationSystem.Infrastructure.DbContext;

namespace DatabaseMigrationSystem.DataAccess.Implementations.User;

public class CreateUserRepository : ICreateUserRepository
{
    private readonly Func<ApplicationDbContext> _contextFactory;

    public CreateUserRepository(Func<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<int> Mutate(Infrastructure.DbContext.Entities.User request, CancellationToken cancellationToken)
    {
        await using var context = _contextFactory();

        var user = await context.Users.AddAsync(request, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return user.Entity.Id;
    }
}