using DatabaseMigrationSystem.Common.Enums;
using DatabaseMigrationSystem.DataAccess.Entity;
using DatabaseMigrationSystem.Infrastructure.DbContext.Entities;

namespace DatabaseMigrationSystem.DataAccess.Interfaces.Migration;

public interface IGetMigrationLogRepository : IGetRepository<int, MigrationLog>
{

}