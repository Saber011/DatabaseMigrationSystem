using AutoMapper;
using DatabaseMigrationSystem.ApplicationServices.Interfaces.Account;
using DatabaseMigrationSystem.Common.Dto;
using DatabaseMigrationSystem.DataAccess.Interfaces.Migration;
using DatabaseMigrationSystem.UseCases.Migration.Queries;
using MediatR;

namespace DatabaseMigrationSystem.UseCases.Migration.Handlers;

public class GetMigrationStatusQueryHandler: IRequestHandler<GetMigrationStatusQuery, MigrationStatusDto>
{
    private readonly IGetCurrentUserInfoService _getCurrentUserInfoService;
    private readonly IGetMigrationLogRepository _getMigrationLogRepository;
    
    public GetMigrationStatusQueryHandler(IGetCurrentUserInfoService getCurrentUserInfoService, IMapper mapper, IGetMigrationLogRepository getMigrationLogRepository)
    {
        _getCurrentUserInfoService = getCurrentUserInfoService;
        _getMigrationLogRepository = getMigrationLogRepository;
    }

    public async Task<MigrationStatusDto> Handle(GetMigrationStatusQuery request, CancellationToken cancellationToken)
    {
        var user = await _getCurrentUserInfoService.Handle(cancellationToken);
        var log = await _getMigrationLogRepository.Get(user.Id, cancellationToken);

        return new MigrationStatusDto()
        {
            CurrentTable = log.TableName,
            Status = log.Status
        };
    }
}