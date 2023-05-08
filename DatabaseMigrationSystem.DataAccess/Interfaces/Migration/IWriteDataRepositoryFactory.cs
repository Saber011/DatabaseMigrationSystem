using DatabaseMigrationSystem.Common.Enums;

namespace DatabaseMigrationSystem.DataAccess.Interfaces.Migration;

public interface IWriteDataRepositoryFactory
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="connectionString"></param>
    public IWriteDataRepository Create(DatabaseType type, string connectionString);
}