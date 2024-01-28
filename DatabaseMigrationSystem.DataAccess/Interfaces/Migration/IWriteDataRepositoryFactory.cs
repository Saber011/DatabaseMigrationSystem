using DatabaseMigrationSystem.Common.Enums;

namespace DatabaseMigrationSystem.DataAccess.Interfaces.Migration;

public interface IWriteDataRepositoryFactory
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="connectionString"></param>
    /// <param name="withBinarySerializable"></param>
    public IWriteDataRepository Create(DatabaseType type, string connectionString, bool withBinarySerializable = false);
}