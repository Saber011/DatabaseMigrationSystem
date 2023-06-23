using AutoMapper;
using DatabaseMigrationSystem.ApplicationServices.Interfaces.Account;
using DatabaseMigrationSystem.DataAccess.Interfaces.Settings;
using DatabaseMigrationSystem.Infrastructure.Validators;
using DatabaseMigrationSystem.UseCases.Settings.Commands;
using DatabaseMigrationSystem.UseCases.Settings.Queries;
using MediatR;

namespace DatabaseMigrationSystem.UseCases.Settings.Handlers;

public class GetSettingsCommandHandler : IRequestHandler<GetSettingsQuery, Infrastructure.DbContext.Entities.Settings>
{
    private readonly IGetSettingsRepository _getSettingsRepository;
    private readonly IMapper _mapper;
    private readonly IGetCurrentUserInfoService _getCurrentUserInfoService;
    
    public GetSettingsCommandHandler( IMapper mapper, IGetCurrentUserInfoService getCurrentUserInfoService, IGetSettingsRepository getSettingsRepository)
    {
        _mapper = mapper;
        _getCurrentUserInfoService = getCurrentUserInfoService;
        _getSettingsRepository = getSettingsRepository;
    }
    
    public async Task<Infrastructure.DbContext.Entities.Settings> Handle(GetSettingsQuery request, CancellationToken cancellationToken)
    {

        var currentUser = await _getCurrentUserInfoService.Handle(cancellationToken);

        var settings = await _getSettingsRepository.Get(currentUser.Id, cancellationToken);

        return settings;
    }
}