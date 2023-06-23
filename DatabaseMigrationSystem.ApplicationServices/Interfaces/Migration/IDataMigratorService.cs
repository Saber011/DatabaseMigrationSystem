using DatabaseMigrationSystem.Common.Dto.Request;

namespace DatabaseMigrationSystem.ApplicationServices.Interfaces.Migration;

public interface IDataMigratorService: IApplicationService<StartDataMigrate, bool>
{
    /// <summary>
    /// 
    /// </summary>
    public Task CancelMigration(int userId);
}