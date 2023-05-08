using DatabaseMigrationSystem.DataAccess.Interfaces.User;
using DatabaseMigrationSystem.Infrastructure.DbContext;

namespace DatabaseMigrationSystem.DataAccess.Implementations.User;

public class UpdateUserInfoRepository : IUpdateUserInfoRepository
{
    private readonly Func<ApplicationDbContext> _contextFactory;

    public UpdateUserInfoRepository(Func<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task Mutate(Infrastructure.DbContext.Entities.User request, CancellationToken cancellationToken)
    {
        await using var context = _contextFactory();
        context.Users.Update(request);

        await context.SaveChangesAsync(cancellationToken);
    }
}