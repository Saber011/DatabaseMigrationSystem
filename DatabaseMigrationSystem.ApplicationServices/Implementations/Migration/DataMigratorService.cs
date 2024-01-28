using System.Collections.Concurrent;
using Action.Platform.Common.Exceptions;
using DatabaseMigrationSystem.ApplicationServices.Interfaces.Migration;
using DatabaseMigrationSystem.Common.Dto.Request;
using DatabaseMigrationSystem.Common.Enums;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using DatabaseMigrationSystem.Infrastructure.DbContext.Entities;
using DatabaseMigrationSystem.Infrastructure.Extentions;
using DatabaseMigrationSystem.Infrastructure.services;
using DatabaseMigrationSystem.Utils;

namespace DatabaseMigrationSystem.ApplicationServices.Implementations.Migration;

public class DataMigratorService : IDataMigratorService
{
    private readonly IReadDataRepositoryFactory _readDataRepositoryFactory;
    private readonly IWriteDataRepositoryFactory _writeDataRepositoryFactory;
    private readonly IWriteMigrationLogRepository _writeMigrationLogRepository;
    private readonly ICacheService _cacheService;
    private readonly IGetDataInfoRepositoryFactory _getDataInfoRepositoryFactory;


    public DataMigratorService(
        IReadDataRepositoryFactory readDataRepositoryFactory,
        IWriteDataRepositoryFactory writeDataRepositoryFactory,
        IWriteMigrationLogRepository writeMigrationLogRepository,
        ICacheService cacheService,
        IGetDataInfoRepositoryFactory getDataInfoRepository)
    {
        _readDataRepositoryFactory = readDataRepositoryFactory;
        _writeDataRepositoryFactory = writeDataRepositoryFactory;
        _writeMigrationLogRepository = writeMigrationLogRepository;
        _cacheService = cacheService;
        _getDataInfoRepositoryFactory = getDataInfoRepository;
    }

    public async Task<bool> Handle(StartDataMigrate request, CancellationToken cancellationToken)
    {
        // Создание нового источника токена отмены
        var cts = new CancellationTokenSource();
        var migrationId = Guid.NewGuid();

        var dataInfoRepository = _getDataInfoRepositoryFactory.Create(request.SourceDatabaseType, request.SourceConnectionString);


        var dataInfo = await dataInfoRepository.Get(cancellationToken);

        // Добавление источника токена отмены в словарь
        await _cacheService.SetAsync(request.UserId.ToString(), cts, TimeSpan.FromHours(1));
        var dataQueue = new BlockingCollection<IList<dynamic>>(new ConcurrentQueue<IList<dynamic>>(), boundedCapacity: 1);

        await _writeMigrationLogRepository.Mutate(new MigrationLog
        {
            Schema = request.DestinationSchema,
            TableName = request.DestinationTable,
            Status = MigrationStatus.Start,
            Date = DateTime.UtcNow,
            UserId = request.UserId,
            ImportSessionId = migrationId,
            DataCount = dataInfo.FirstOrDefault(x => x.CollectionName == request.SourceTable)?.DocumentCount ?? 0
        }, cancellationToken);

        var readRepository = _readDataRepositoryFactory.Create(request.SourceDatabaseType, request.SourceConnectionString);
        var writeRepository = _writeDataRepositoryFactory.Create(request.DestinationDatabaseType, request.DestinationConnectionString);
        
         var readTask = readRepository.ReadDataAsync(request.SourceSchema, request.SourceTable, dataQueue);
         var writeTask = Task.Run( () =>
         {
             var log = new MigrationLog
             {
                 Schema = request.DestinationSchema,
                 TableName = request.DestinationTable,
                 Status = MigrationStatus.Processed,
                 Date = DateTime.UtcNow,
                 UserId = request.UserId,
                 ImportSessionId = migrationId,
             };
             RetryHelper.Do(
                 () => writeRepository.WriteDataAsync(request.DestinationSchema, request.DestinationTable, dataQueue, log, cancellationToken).GetAwaiter().GetResult(),
                 (ex, iteration) =>
                 {
                     log.Exception = ex.Message;
                     _writeMigrationLogRepository.Mutate(log, CancellationToken.None).FireAndForget();
                 },
                 2, TimeSpan.FromMilliseconds(10), 2);
             
         }, cts.Token);


        await Task.WhenAll(readTask, writeTask);
        // Удаление источника токена отмены из словаря после завершения миграции
        await _cacheService.RemoveAsync(request.UserId.ToString());
        await _writeMigrationLogRepository.Mutate(new MigrationLog
        {
            Schema = request.DestinationSchema,
            TableName = request.DestinationTable,
            Status = MigrationStatus.Finish,
            Date = DateTime.UtcNow,
            UserId = request.UserId,
            ImportSessionId = migrationId,
        }, CancellationToken.None);
        return true;
    }
    
    public async Task CancelMigration(int userId)
    {
        // Получение источника токена отмены из словаря
        var token = await _cacheService.GetAsync<CancellationTokenSource>(userId.ToString());
        if (token is not null)
        {
             await _writeMigrationLogRepository.Mutate(new MigrationLog
            {
                DataCount = 0,
                Status = MigrationStatus.Cancel,
                Date = DateTime.UtcNow,
                UserId = userId,
            }, token.Token);
            
            // Отмена операции
            token.Cancel();
        }
        else
        {
            throw new BrokenRulesException($"Migration with ID {userId} not found");
        }
    }
}