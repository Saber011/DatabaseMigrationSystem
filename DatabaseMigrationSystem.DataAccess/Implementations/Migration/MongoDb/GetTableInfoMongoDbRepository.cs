using DatabaseMigrationSystem.DataAccess.Entity;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;

namespace DatabaseMigrationSystem.DataAccess.Implementations.Migration.MongoDb;

public class GetTableInfoMongoDbRepository: IGetTableInfoRepository
{
    private readonly string _database;
    private readonly IMongoClient _client;

    public GetTableInfoMongoDbRepository(string connectionString)
    {
        _client = new MongoClient(connectionString);
        var mongoUrl = new MongoUrl(connectionString);
        _database = mongoUrl.DatabaseName;
    }
    
    public async Task<List<TableInfo>> Get(CancellationToken cancellationToken)
    {
        var tableInfoList = new List<TableInfo>();
        var database = _client.GetDatabase(_database);
        var collections = await database.ListCollectionNamesAsync(cancellationToken: cancellationToken);
        var collectionNames = await collections.ToListAsync(cancellationToken);

        foreach (var collectionName in collectionNames)
        {
            var mongoCollection = database.GetCollection<BsonDocument>(collectionName);

            // Получение документа с максимальным количеством полей
            var documentWithMaxFields = await mongoCollection.Find(new BsonDocument())
                                                             .Sort("{ $natural: -1 }")
                                                             .Limit(1)
                                                             .FirstOrDefaultAsync(cancellationToken);

            if (documentWithMaxFields != null)
            {
                var columnNames = documentWithMaxFields.Names.ToList();
                var documentCount =
                    await mongoCollection.CountDocumentsAsync(new BsonDocument(), cancellationToken: cancellationToken);

                // Создание списка ColumnInfo
                var columns = columnNames.Select(name => new ColumnInfo { ColumnName = name }).ToList();

                tableInfoList.Add(new TableInfo
                {
                    TableName = collectionName,
                    Columns = columns,
                    RowCount = documentCount
                });
            }
        }

        return tableInfoList;
    }
}
