using DatabaseMigrationSystem.Infrastructure.DbContext.Entities;

namespace DatabaseMigrationSystem.DataAccess.Interfaces.User;

public interface IGetUserTokensRepository : IGetRepository<int, UserToken[]>
{
    
}