using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Concurrent;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using DatabaseMigrationSystem.Infrastructure.DbContext;

public class ReadDataMongoDbRepository : ReadDataRepository
{
    private readonly IMongoDatabase _database;

    public ReadDataMongoDbRepository(string connectionString)
    {
        var client = new MongoClient(connectionString);
        var mongoUrl = MongoUrl.Create(connectionString);
        _database = client.GetDatabase(mongoUrl.DatabaseName);
    }

    public override async Task ReadDataAsync(string schema, string table, BlockingCollection<IList<dynamic>> dataQueue)
    {
        var collection = _database.GetCollection<BsonDocument>(table);

        using (var cursor = await collection.FindAsync(new BsonDocument()))
        {
            while (await cursor.MoveNextAsync())
            {
                var batch = cursor.Current;
                var dataList = new List<dynamic>();

                foreach (var document in batch)
                {
                    // Преобразуем BsonDocument в динамический объект
                    var dynamicObject = BsonTypeMapper.MapToDotNetValue(document);
                    dataList.Add(dynamicObject);
                }

                if (dataList.Count > 0)
                {
                    dataQueue.Add(dataList);
                }
            }
        }

        dataQueue.CompleteAdding();
    }
}