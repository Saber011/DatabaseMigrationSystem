namespace DatabaseMigrationSystem.DataAccess.Interfaces.User;

public interface IGetByLoginUserRepository : IGetRepository<string, Infrastructure.DbContext.Entities.User>
{
    
}