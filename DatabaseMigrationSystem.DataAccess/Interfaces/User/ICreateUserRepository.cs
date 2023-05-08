namespace DatabaseMigrationSystem.DataAccess.Interfaces.User;

public interface ICreateUserRepository : IMutateRepository<Infrastructure.DbContext.Entities.User, int>
{
    
}