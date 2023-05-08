using DatabaseMigrationSystem.Common.Dto.Request;

namespace DatabaseMigrationSystem.ApplicationServices.Interfaces.Migration;

public interface IDataMigratorService: IApplicationService<StartDataMigrate, bool>
{
    
}