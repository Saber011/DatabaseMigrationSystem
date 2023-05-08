using AutoMapper;
using DatabaseMigrationSystem.ApplicationServices.Interfaces.Account;
using DatabaseMigrationSystem.ApplicationServices.Interfaces.Migration;
using DatabaseMigrationSystem.Common.Dto;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using DatabaseMigrationSystem.DataAccess.Interfaces.Settings;
using DatabaseMigrationSystem.UseCases.Migration.Queries;
using MediatR;

namespace DatabaseMigrationSystem.UseCases.Migration.Handlers;

public class GetSourceTablesQueryHandler: IRequestHandler<GetSourceTablesQuery, TableInfosDto>
{
    private readonly IGetCurrentUserInfoService _getCurrentUserInfoService;
    private readonly IGetSettingsRepository _getSettingsRepository;
    private readonly IMapper _mapper;
    private readonly IGetTableInfoRepositoryFactory _getTableInfoRepositoryFactory;
    
    public GetSourceTablesQueryHandler(IGetCurrentUserInfoService getCurrentUserInfoService, IGetSettingsRepository getSettingsRepository, IMapper mapper, IGetTableInfoRepositoryFactory getTableInfoRepositoryFactory)
    {
        _getCurrentUserInfoService = getCurrentUserInfoService;
        _getSettingsRepository = getSettingsRepository;
        _mapper = mapper;
        _getTableInfoRepositoryFactory = getTableInfoRepositoryFactory;
    }

    public async Task<TableInfosDto> Handle(GetSourceTablesQuery request, CancellationToken cancellationToken)
    {
       var user = await _getCurrentUserInfoService.Handle(cancellationToken);

       var settings = await _getSettingsRepository.Get(user.Id, cancellationToken);
       var sourceTableInfoService = _getTableInfoRepositoryFactory.Create(settings.SourceDatabaseType, settings.SourceConnectionString);
       var destinationTableInfoService = _getTableInfoRepositoryFactory.Create(settings.DestinationDatabaseType, settings.DestinationConnectionString);

       var sourceTables = await sourceTableInfoService.Get(cancellationToken);
       var destinationTables = await destinationTableInfoService.Get(cancellationToken);
       
       return new TableInfosDto
       {
           SourceTables = _mapper.Map<List<TableInfoDto>>(sourceTables),
           DestinationTables = _mapper.Map<List<TableInfoDto>>(destinationTables)
       };
    }
}