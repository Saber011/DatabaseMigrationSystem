﻿using AutoMapper;
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

    public GetMigrationJournalDataQueryHandler(IGetCurrentUserInfoService getCurrentUserInfoService, IMapper mapper,
        IGetUserMigrationDataRepository getUserMigrationDataRepository, IGetSettingsRepository getSettingsRepository)
    {
        _getCurrentUserInfoService = getCurrentUserInfoService;
        _getUserMigrationDataRepository = getUserMigrationDataRepository;
    }

    public async Task<List<UserMigrationData>> Handle(GetMigrationJournalDataQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _getCurrentUserInfoService.Handle(cancellationToken);
        var data = await _getUserMigrationDataRepository.Get((user.Id,0, 100), cancellationToken);

        return data.OrderByDescending(x => x.Date).ToList();
    }
}