using DatabaseMigrationSystem.DataAccess.Entity;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Migration.MongoDb;

public class GetDataInfoMongoDbRepository : IGetDataInfoRepository
{
    private readonly string _connectionString;
    private readonly IMongoClient _client;

    public GetDataInfoMongoDbRepository(string connectionString)
    {
        _client = new MongoClient(connectionString);
        _connectionString = connectionString;
    }
    
    public async Task<CollectionInfo[]> Get(CancellationToken cancellationToken)
    {
        
        var databases = await _client.ListDatabasesAsync(cancellationToken).Result.ToListAsync(cancellationToken: cancellationToken);
        var collectionInfoList = new List<CollectionInfo>();
        foreach (var db in databases
                     .Select(database => database["name"].AsString)
                     .Select(dbName => _client.GetDatabase(dbName)))
        {
            var collections = await db.ListCollectionsAsync(cancellationToken: cancellationToken);
            var collectionNames = await collections.ToListAsync(cancellationToken);

            foreach (var collection in collectionNames)
            {
                var collectionName = collection["name"].AsString;
                var mongoCollection = db.GetCollection<BsonDocument>(collectionName);

                // Получение количества документов в коллекции
                var documentCount =
                    await mongoCollection.CountDocumentsAsync(new BsonDocument(), cancellationToken: cancellationToken);

                collectionInfoList.Add(new CollectionInfo
                {
                    CollectionName = collectionName,
                    DocumentCount = documentCount
                });
            }
        }

        return collectionInfoList.ToArray();
    }
}