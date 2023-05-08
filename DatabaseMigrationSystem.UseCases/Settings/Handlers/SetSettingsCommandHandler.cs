using AutoMapper;
using DatabaseMigrationSystem.ApplicationServices.Interfaces.Account;
using DatabaseMigrationSystem.DataAccess.Interfaces.Settings;
using DatabaseMigrationSystem.Infrastructure.Validators;
using DatabaseMigrationSystem.UseCases.Settings.Commands;
using MediatR;

namespace DatabaseMigrationSystem.UseCases.Settings.Handlers;

public class SetSettingsCommandHandler : IRequestHandler<SetSettingsCommand>
{
    private readonly IConnectionValidator _connectionValidator;
    private readonly ISetSettingsRepository _setSettingsRepository;
    private readonly IMapper _mapper;
    private readonly IGetCurrentUserInfoService _getCurrentUserInfoService;
    
    public SetSettingsCommandHandler(IConnectionValidator connectionValidator, ISetSettingsRepository setSettingsRepository, IMapper mapper, IGetCurrentUserInfoService getCurrentUserInfoService)
    {
        _connectionValidator = connectionValidator;
        _setSettingsRepository = setSettingsRepository;
        _mapper = mapper;
        _getCurrentUserInfoService = getCurrentUserInfoService;
    }
    
    public async Task<Unit> Handle(SetSettingsCommand request, CancellationToken cancellationToken)
    {
        await _connectionValidator.ValidateConnection(request.SourceDatabaseType, request.SourceConnectionString);
        await _connectionValidator.ValidateConnection(request.DestinationDatabaseType, request.DestinationConnectionString);

        var settings = _mapper.Map<Infrastructure.DbContext.Entities.Settings>(request);

        var currentUser = await _getCurrentUserInfoService.Handle(cancellationToken);

        settings.UserId = currentUser.Id;

        await _setSettingsRepository.Mutate(settings, cancellationToken);

        return default;
    }
}