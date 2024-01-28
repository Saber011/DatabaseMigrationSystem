using System.Collections.Concurrent;
using DatabaseMigrationSystem.Infrastructure.DbContext.Entities;

namespace DatabaseMigrationSystem.DataAccess.Interfaces.Migration;

public interface IWriteDataRepository
{
    Task WriteDataAsync(string schema, string table, BlockingCollection<IList<dynamic>> dataQueue, MigrationLog migrationLog, CancellationToken cancellationToken);
}