using AutoMapper;
using DatabaseMigrationSystem.ApplicationServices.Interfaces.Account;
using DatabaseMigrationSystem.ApplicationServices.Interfaces.Migration;
using DatabaseMigrationSystem.UseCases.Migration.Commands;
using MediatR;

namespace DatabaseMigrationSystem.UseCases.Migration.Handlers;

/// <summary>
/// 
/// </summary>
public class CancelMigrateCommandHandler : IRequestHandler<CancelMigrateCommand>
{
    private readonly IGetCurrentUserInfoService _getCurrentUserInfoService;
    private readonly IDataMigratorService _dataMigratorService;
    
    public CancelMigrateCommandHandler(IGetCurrentUserInfoService getCurrentUserInfoService, IMapper mapper, IDataMigratorService dataMigratorService)
    {
        _getCurrentUserInfoService = getCurrentUserInfoService;
        _dataMigratorService = dataMigratorService;
    }

    public async Task<Unit> Handle(CancelMigrateCommand request, CancellationToken cancellationToken)
    {
        var user = await _getCurrentUserInfoService.Handle(cancellationToken);

        await _dataMigratorService.CancelMigration(user.Id);
        
        return default;
    }
}