using AutoMapper;
using DatabaseMigrationSystem.Common.Dto;
using DatabaseMigrationSystem.DataAccess.Interfaces.Settings;
using DatabaseMigrationSystem.DataAccess.Interfaces.User;
using DatabaseMigrationSystem.UseCases.User.Queries;
using MediatR;

namespace DatabaseMigrationSystem.UseCases.User.Handlers;

public class GetUserQueryHandler: IRequestHandler<GetUserQuery, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IGetByIdUserGetRepository _getByIdUserGetRepository;
    private readonly IGetUserTokensRepository _getUserTokensRepository;
    private readonly IGetUserRolesRepository _getUserRolesRepository;
    private readonly IGetSettingsRepository _getSettingsRepository;
    
    public GetUserQueryHandler(IMapper mapper, IGetByIdUserGetRepository getByIdUserGetRepository, IGetUserTokensRepository getUserTokensRepository, IGetUserRolesRepository getUserRolesRepository, IGetSettingsRepository getSettingsRepository)
    {
        _mapper = mapper;
        _getByIdUserGetRepository = getByIdUserGetRepository;
        _getUserTokensRepository = getUserTokensRepository;
        _getUserRolesRepository = getUserRolesRepository;
        _getSettingsRepository = getSettingsRepository;
    }
    
    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _getByIdUserGetRepository.Get(request.UserId, cancellationToken);
        var token = await _getUserTokensRepository.Get(user.Id, cancellationToken);
        user.UserTokens = token;
        var roles = await _getUserRolesRepository.Get(user.Id, cancellationToken);
        user.Roles = roles;
        user.Settings = await _getSettingsRepository.Get(user.Id, cancellationToken);
        
        return _mapper.Map<UserDto>(user);
    }
}