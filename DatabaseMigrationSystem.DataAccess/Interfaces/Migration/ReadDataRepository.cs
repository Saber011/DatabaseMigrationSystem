using System.Collections.Concurrent;

namespace DatabaseMigrationSystem.DataAccess.Interfaces.Migration;

public abstract class ReadDataRepository
{
    protected int BatchSize = 1000;
    
    public abstract Task ReadDataAsync(string schema, string table, BlockingCollection<IList<dynamic>> dataQueue);
}