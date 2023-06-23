using AutoMapper;
using DatabaseMigrationSystem.ApplicationServices.Interfaces.Account;
using DatabaseMigrationSystem.Common.Dto;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using DatabaseMigrationSystem.DataAccess.Interfaces.Settings;
using DatabaseMigrationSystem.UseCases.Migration.Queries;
using MediatR;

namespace DatabaseMigrationSystem.UseCases.Migration.Handlers;

public class
    GetMigrationJournalDataQueryHandler : IRequestHandler<GetMigrationJournalDataQuery, List<UserMigrationData>>
{
    private readonly IGetCurrentUserInfoService _getCurrentUserInfoService;
    private readonly IGetUserMigrationDataRepository _getUserMigrationDataRepository;
    private readonly IGetSettingsRepository _getSettingsRepository;

    public GetMigrationJournalDataQueryHandler(IGetCurrentUserInfoService getCurrentUserInfoService, IMapper mapper,
        IGetUserMigrationDataRepository getUserMigrationDataRepository, IGetSettingsRepository getSettingsRepository)
    {
        _getCurrentUserInfoService = getCurrentUserInfoService;
        _getUserMigrationDataRepository = getUserMigrationDataRepository;
        _getSettingsRepository = getSettingsRepository;
    }

    public async Task<List<UserMigrationData>> Handle(GetMigrationJournalDataQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _getCurrentUserInfoService.Handle(cancellationToken);
        var data = await _getUserMigrationDataRepository.Get(user.Id, cancellationToken);
        var settings = await _getSettingsRepository.Get(user.Id, cancellationToken);

        foreach (var item in data)
        {
            item.DestinationDatabase = settings.DestinationDatabaseType.ToString();
            item.SourceDatabase = settings.SourceDatabaseType.ToString();
        }
        return data;
    }
}