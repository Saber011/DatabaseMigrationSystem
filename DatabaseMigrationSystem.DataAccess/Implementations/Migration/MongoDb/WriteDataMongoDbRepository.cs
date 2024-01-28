using System.Collections.Concurrent;
using MongoDB.Bson;
using MongoDB.Driver;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using DatabaseMigrationSystem.Infrastructure.DbContext;
using DatabaseMigrationSystem.Infrastructure.DbContext.Entities;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Migration.MongoDb;

public class WriteDataMongoDbRepository : IWriteDataRepository
{
    private readonly IMongoDatabase _database;
    private readonly Func<ApplicationDbContext> _contextFactory;

    public WriteDataMongoDbRepository(string connectionString, Func<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
        var client = new MongoClient(connectionString);
        var mongoUrl = MongoUrl.Create(connectionString);
        _database = client.GetDatabase(mongoUrl.DatabaseName);
    }

    public async Task WriteDataAsync(string schema, string table, BlockingCollection<IList<dynamic>> dataQueue, MigrationLog migrationLog,  CancellationToken cancellationToken)
    {
        var collection = _database.GetCollection<BsonDocument>(table);

        foreach (var batch in dataQueue.GetConsumingEnumerable(cancellationToken))
        {
            var documents = new List<BsonDocument>();

            foreach (IDictionary<string, object> dataRow in batch)
            {
                var document = new BsonDocument();
                foreach (var keyValuePair in dataRow)
                {
                    document[keyValuePair.Key] = BsonValue.Create(keyValuePair.Value);
                }
                documents.Add(document);
            }

            if (documents.Count > 0)
            {
                await collection.InsertManyAsync(documents, cancellationToken: cancellationToken);
            }
            
            migrationLog.DataCount = batch.Count;
            migrationLog.Date = DateTime.UtcNow;
            await WriteLog(migrationLog);
        }
        
    }
    
    private async Task WriteLog(MigrationLog log)
    {
        var context = _contextFactory();

        context.MigrationLog.Add(log);

        await context.SaveChangesAsync();
    }
}