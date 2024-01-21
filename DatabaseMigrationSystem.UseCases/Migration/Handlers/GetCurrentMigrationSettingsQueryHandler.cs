using DatabaseMigrationSystem.ApplicationServices.Interfaces.Account;
using DatabaseMigrationSystem.Common.Dto;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using DatabaseMigrationSystem.DataAccess.Interfaces.Settings;
using DatabaseMigrationSystem.UseCases.Migration.Queries;
using MediatR;

namespace DatabaseMigrationSystem.UseCases.Migration.Handlers;

public class GetCurrentMigrationSettingsQueryHandler : IRequestHandler<GetCurrentMigrationSettingsQuery, CurrentMigrationSettingsDto>
{
    private readonly IGetCurrentUserInfoService _getCurrentUserInfoService;
    private readonly IGetDataInfoRepositoryFactory _getDataInfoRepositoryFactory;
    private readonly IGetSettingsRepository _getSettingsRepository;
    
    public GetCurrentMigrationSettingsQueryHandler(IGetCurrentUserInfoService getCurrentUserInfoService, IGetDataInfoRepositoryFactory getDataInfoRepositoryFactory, IGetSettingsRepository getSettingsRepository)
    {
        _getCurrentUserInfoService = getCurrentUserInfoService;
        _getDataInfoRepositoryFactory = getDataInfoRepositoryFactory;
        _getSettingsRepository = getSettingsRepository;
    }

    public async Task<CurrentMigrationSettingsDto> Handle(GetCurrentMigrationSettingsQuery request, CancellationToken cancellationToken)
    {

        var userInfo = await _getCurrentUserInfoService.Handle(cancellationToken);

        var settings = await _getSettingsRepository.Get(userInfo.Id, cancellationToken);

        var sourceRepo = _getDataInfoRepositoryFactory.Create(settings.SourceDatabaseType,
            settings.SourceConnectionString);
        
        var destinationRepo = _getDataInfoRepositoryFactory.Create(settings.DestinationDatabaseType,
            settings.DestinationConnectionString);

        var data = await sourceRepo.Get(cancellationToken);
        var data2 = await destinationRepo.Get(cancellationToken);

        return new CurrentMigrationSettingsDto()
        {
            DestinationDatabaseType = settings.DestinationDatabaseType,
            SourceDatabaseType = settings.SourceDatabaseType,
            DestinationDatabaseDataInfo = $"Таблиц: {data.Length} Записей: {data.Sum(x => x.DocumentCount)}",
            SourceDatabaseDataInfo = $"Таблиц: {data2.Length}  Записей: {data2.Sum(x => x.DocumentCount)}"
        };
    }
}