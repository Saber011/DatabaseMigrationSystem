using DatabaseMigrationSystem.Common.Enums;

namespace DatabaseMigrationSystem.DataAccess.Interfaces.Migration;

public interface IReadDataRepositoryFactory
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="connectionString"></param>
    public ReadDataRepository Create(DatabaseType type, string connectionString);
}