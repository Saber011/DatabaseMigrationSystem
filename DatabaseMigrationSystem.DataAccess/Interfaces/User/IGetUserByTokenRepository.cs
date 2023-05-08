namespace DatabaseMigrationSystem.DataAccess.Interfaces.User;

public interface IGetUserByTokenRepository : IGetRepository<string, Infrastructure.DbContext.Entities.User>
{
    
}