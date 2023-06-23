using DatabaseMigrationSystem.ApplicationServices.Interfaces.Account;
using DatabaseMigrationSystem.ApplicationServices.Interfaces.Migration;
using DatabaseMigrationSystem.Common.Dto.Request;
using DatabaseMigrationSystem.DataAccess.Interfaces.Settings;
using DatabaseMigrationSystem.Infrastructure.Extentions;
using DatabaseMigrationSystem.UseCases.Migration.Commands;
using MediatR;

namespace DatabaseMigrationSystem.UseCases.Migration.Handlers;

public class MigrateTableCommandHandler: IRequestHandler<MigrateTableCommand>
{
    private readonly IDataMigratorService _dataMigratorService;
    private readonly IGetCurrentUserInfoService _getCurrentUserInfoService;
    private readonly IGetSettingsRepository _getSettingsRepository;
    public MigrateTableCommandHandler(IDataMigratorService dataMigratorService, IGetCurrentUserInfoService getCurrentUserInfoService, IGetSettingsRepository getSettingsRepository)
    {
        _dataMigratorService = dataMigratorService;
        _getCurrentUserInfoService = getCurrentUserInfoService;
        _getSettingsRepository = getSettingsRepository;
    }

    public async Task<Unit> Handle(MigrateTableCommand request, CancellationToken cancellationToken)
    {
        var user = await _getCurrentUserInfoService.Handle(cancellationToken);

        var settings = await _getSettingsRepository.Get(user.Id, cancellationToken);

        foreach (var table in request.Tables)
        {
            var dataMigrateRequest = new StartDataMigrate
            {
                SourceSchema = table.SourceSchema,
                SourceConnectionString = settings.SourceConnectionString,
                SourceTable = table.SourceTable,
                SourceDatabaseType = settings.SourceDatabaseType,
                DestinationSchema = table.DestinationSchema,
                DestinationTable = table.DestinationTable,
                DestinationConnectionString = settings.DestinationConnectionString,
                DestinationDatabaseType = settings.DestinationDatabaseType,
                UserId = user.Id
            };

            _dataMigratorService.Handle(dataMigrateRequest, cancellationToken).FireAndForget();;
        }

        return default;
    }
}