using Dapper;
using DatabaseMigrationSystem.DataAccess.Entity;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using MongoDB.Bson;
using MongoDB.Driver;
using MySqlConnector;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Migration.MongoDb;

public class GetTableInfoMongoDbRepository: IGetTableInfoRepository
{
    private readonly string _connectionString;
    private readonly IMongoClient _client;

    public GetTableInfoMongoDbRepository(string connectionString)
    {
        _client = new MongoClient(connectionString);
        _connectionString = connectionString;
    }
    
    public async Task<List<TableInfo>> Get(CancellationToken cancellationToken)
    {
        var database = _client.GetDatabase(_connectionString);
        var collections = await database.ListCollectionsAsync(cancellationToken: cancellationToken);
        var collectionNames = await collections.ToListAsync(cancellationToken);

        var collectionInfoList = new List<CollectionInfo>();

        foreach (var collection in collectionNames)
        {
            var collectionName = collection["name"].AsString;
            var mongoCollection = database.GetCollection<BsonDocument>(collectionName);

            // Получение количества документов в коллекции
            var documentCount = await mongoCollection.CountDocumentsAsync(new BsonDocument(), cancellationToken: cancellationToken);

            collectionInfoList.Add(new CollectionInfo
            {
                CollectionName = collectionName,
                DocumentCount = documentCount
            });
        }

        return new List<TableInfo>();
    }
}

