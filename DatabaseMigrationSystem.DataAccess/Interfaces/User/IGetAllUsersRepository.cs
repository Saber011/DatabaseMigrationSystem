namespace DatabaseMigrationSystem.DataAccess.Interfaces.User;

public interface IGetAllUsersRepository : IGetRepository<Infrastructure.DbContext.Entities.User[]>
{
    
}