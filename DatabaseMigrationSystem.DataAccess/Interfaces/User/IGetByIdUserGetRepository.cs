namespace DatabaseMigrationSystem.DataAccess.Interfaces.User;

/// <summary>
/// Получить пользователя по id
/// </summary>
public interface IGetByIdUserGetRepository : IGetRepository<int, Infrastructure.DbContext.Entities.User>
{
    
}