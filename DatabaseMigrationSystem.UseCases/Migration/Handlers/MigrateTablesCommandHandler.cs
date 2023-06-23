using DatabaseMigrationSystem.ApplicationServices.Interfaces.Account;
using DatabaseMigrationSystem.ApplicationServices.Interfaces.Migration;
using DatabaseMigrationSystem.Common.Dto.Request;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using DatabaseMigrationSystem.DataAccess.Interfaces.Settings;
using DatabaseMigrationSystem.Infrastructure.Extentions;
using DatabaseMigrationSystem.UseCases.Migration.Commands;
using MediatR;

namespace DatabaseMigrationSystem.UseCases.Migration.Handlers;

public class MigrateTablesCommandHandler: IRequestHandler<MigrateTablesCommand>
{
    private readonly IDataMigratorService _dataMigratorService;
    private readonly IGetCurrentUserInfoService _getCurrentUserInfoService;
    private readonly IGetSettingsRepository _getSettingsRepository;
    private readonly IGetTableInfoRepositoryFactory _getTableInfoRepositoryFactory;
    public MigrateTablesCommandHandler(IDataMigratorService dataMigratorService, IGetCurrentUserInfoService getCurrentUserInfoService, IGetSettingsRepository getSettingsRepository, IGetTableInfoRepositoryFactory getTableInfoRepositoryFactory)
    {
        _dataMigratorService = dataMigratorService;
        _getCurrentUserInfoService = getCurrentUserInfoService;
        _getSettingsRepository = getSettingsRepository;
        _getTableInfoRepositoryFactory = getTableInfoRepositoryFactory;
    }

    public async Task<Unit> Handle(MigrateTablesCommand request, CancellationToken cancellationToken)
    {
        var user = await _getCurrentUserInfoService.Handle(cancellationToken);

        var settings = await _getSettingsRepository.Get(user.Id, cancellationToken);
        var getTableInfoRepository2 =
            _getTableInfoRepositoryFactory.Create(settings.DestinationDatabaseType,
                settings.DestinationConnectionString);
        var getTableInfoRepository =
            _getTableInfoRepositoryFactory.Create(settings.SourceDatabaseType,
                settings.SourceConnectionString);
       var tables = await getTableInfoRepository.Get(cancellationToken);
       var table2 = await getTableInfoRepository2.Get(cancellationToken);
       
       var avalibleTables = tables
           .Where(x => table2.Any(y => string.Equals(y.TableName, x.TableName, StringComparison.CurrentCultureIgnoreCase)))
           .ToArray();

       foreach (var table in avalibleTables)
        {
            var dataMigrateRequest = new StartDataMigrate
            {
                SourceSchema = table.Schema,
                SourceConnectionString = settings.SourceConnectionString,
                SourceTable = table.TableName,
                SourceDatabaseType = settings.SourceDatabaseType,
                DestinationSchema = table2.FirstOrDefault(x => string.Equals(x.TableName, table.TableName, StringComparison.CurrentCultureIgnoreCase))!.Schema,
                DestinationTable = table2.FirstOrDefault(x => string.Equals(x.TableName, table.TableName, StringComparison.CurrentCultureIgnoreCase))!.TableName,
                DestinationConnectionString = settings.DestinationConnectionString,
                DestinationDatabaseType = settings.DestinationDatabaseType,
                UserId = user.Id
            };

             _dataMigratorService.Handle(dataMigrateRequest, cancellationToken).FireAndForget();
        }

        return default;
    }
}