using DatabaseMigrationSystem.DataAccess.Entity;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Migration.MongoDb;

public class GetDataInfoMongoDbRepository : IGetDataInfoRepository
{
    private readonly IMongoClient _client;
    private readonly string _database;

    public GetDataInfoMongoDbRepository(string connectionString)
    {
        _client = new MongoClient(connectionString);
        var mongoUrl = new MongoUrl(connectionString);
        _database = mongoUrl.DatabaseName;
    }
    
    public async Task<CollectionInfo[]> Get(CancellationToken cancellationToken)
    {
        var collectionInfoList = new List<CollectionInfo>();
        var db= _client.GetDatabase(_database);
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

        return collectionInfoList.ToArray();
    }
}