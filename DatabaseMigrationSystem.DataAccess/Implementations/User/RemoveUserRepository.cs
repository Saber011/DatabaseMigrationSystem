using DatabaseMigrationSystem.DataAccess.Interfaces.User;
using DatabaseMigrationSystem.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMigrationSystem.DataAccess.Implementations.User;

public class RemoveUserRepository : IRemoveUserRepository
{
    private readonly Func<ApplicationDbContext> _contextFactory;

    public RemoveUserRepository(Func<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task Mutate(int request, CancellationToken cancellationToken)
    {
        await using var context = _contextFactory();

        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == request, cancellationToken);

        if (user is not null)
        {
            context.Users.Remove(user);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}