using System.Collections.Concurrent;
using DatabaseMigrationSystem.ApplicationServices.Interfaces.Migration;
using DatabaseMigrationSystem.Common.Dto.Request;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using DatabaseMigrationSystem.Utils;

namespace DatabaseMigrationSystem.ApplicationServices.Implementations.Migration;

public class DataMigratorService : IDataMigratorService
{
    private readonly IReadDataRepositoryFactory _readDataRepositoryFactory;
    private readonly IWriteDataRepositoryFactory _writeDataRepositoryFactory;

    public DataMigratorService(IReadDataRepositoryFactory readDataRepositoryFactory, IWriteDataRepositoryFactory writeDataRepositoryFactory)
    {
        _readDataRepositoryFactory = readDataRepositoryFactory;
        _writeDataRepositoryFactory = writeDataRepositoryFactory;
    }

    public async Task<bool> Handle(StartDataMigrate request, CancellationToken cancellationToken)
    {
        var dataQueue = new BlockingCollection<IList<dynamic>>(new ConcurrentQueue<IList<dynamic>>(), boundedCapacity: 1);

        var readRepository = _readDataRepositoryFactory.Create(request.SourceDatabaseType, request.SourceConnectionString);
        var writeRepository = _writeDataRepositoryFactory.Create(request.DestinationDatabaseType, request.DestinationConnectionString);
        
         await readRepository.ReadDataAsync( request.SourceSchema,  request.SourceTable, dataQueue);
         await writeRepository.WriteDataAsync(request.DestinationSchema, request.DestinationTable, dataQueue);
         var readTask = Task.Run(() => readRepository.ReadDataAsync( request.SourceSchema,  request.SourceTable, dataQueue), cancellationToken);
         var writeTask = Task.Run(() =>
         {
             RetryHelper.Do(
                 () => writeRepository.WriteDataAsync(request.DestinationSchema, request.DestinationTable, dataQueue),
                 (ex, iteration) =>
                 {
                     
                 },
                 2, TimeSpan.FromMilliseconds(10), 2);
             
         }, cancellationToken);
        
        await Task.WhenAll(readTask, writeTask);
        return true;
    }
}