using System.Collections.Concurrent;

namespace DatabaseMigrationSystem.DataAccess.Interfaces.Migration;

public interface IWriteDataRepository
{
    Task WriteDataAsync(string schema, string table, BlockingCollection<IList<dynamic>> dataQueue);
}