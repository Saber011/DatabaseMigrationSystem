using DatabaseMigrationSystem.Common.Enums;

namespace DatabaseMigrationSystem.DataAccess.Interfaces.Migration;

public interface IGetTableInfoRepositoryFactory
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="connectionString"></param>
    public IGetTableInfoRepository Create(DatabaseType type, string connectionString);
}