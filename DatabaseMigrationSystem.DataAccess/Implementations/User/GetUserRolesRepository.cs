using DatabaseMigrationSystem.DataAccess.Interfaces.User;
using DatabaseMigrationSystem.Infrastructure.DbContext;
using DatabaseMigrationSystem.Infrastructure.DbContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseMigrationSystem.DataAccess.Implementations.User;

public class GetUserRolesRepository : IGetUserRolesRepository
{
    private readonly Func<ApplicationDbContext> _contextFactory;

    public GetUserRolesRepository(Func<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<UserRoles> Get(int request, CancellationToken cancellationToken)
    {
        await using var context = _contextFactory();

        return await context.UserRoles.FirstOrDefaultAsync(x => x.UserId == request, cancellationToken);
    }
}