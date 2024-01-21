using DatabaseMigrationSystem.Common.Enums;

namespace DatabaseMigrationSystem.DataAccess.Interfaces.Migration;

public interface IGetDataInfoRepositoryFactory
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="connectionString"></param>
    public IGetDataInfoRepository Create(DatabaseType type, string connectionString);
}