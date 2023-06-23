using DatabaseMigrationSystem.Common.Dto;

namespace DatabaseMigrationSystem.DataAccess.Interfaces.Migration;

public interface IGetUserMigrationDataRepository : IGetRepository<int, List<UserMigrationData>>
{
    
}