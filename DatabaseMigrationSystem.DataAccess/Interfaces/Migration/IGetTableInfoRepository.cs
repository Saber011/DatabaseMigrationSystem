using DatabaseMigrationSystem.Common.Enums;
using DatabaseMigrationSystem.DataAccess.Entity;

namespace DatabaseMigrationSystem.DataAccess.Interfaces.Migration;

public interface IGetTableInfoRepository : IGetRepository<List<TableInfo>>
{

}