using DatabaseMigrationSystem.Common.Dto;

namespace DatabaseMigrationSystem.DataAccess.Interfaces.Migration;

public interface IGetUserMigrationDataRepository : IGetRepository<(int userId, int page, int pageSize), List<UserMigrationData>>
{
    
}