using AutoMapper;
using DatabaseMigrationSystem.ApplicationServices.Interfaces.Account;
using DatabaseMigrationSystem.DataAccess.Interfaces.User;
using DatabaseMigrationSystem.Infrastructure.DbContext.Entities;
using DatabaseMigrationSystem.UseCases.Account.Commands;
using MediatR;

namespace DatabaseMigrationSystem.UseCases.Account.Handlers;

public class RevokeTokenCommandHandler: IRequestHandler<RevokeTokenCommand>
{
    private readonly IMapper _mapper;
    private readonly IGetUserByTokenRepository _getUserByTokenRepository;
    private readonly IGetUserTokensRepository _getUserTokensRepository;
    private readonly IUpdateUserTokenRepository _updateUserTokenRepository;

    public RevokeTokenCommandHandler(IUpdateUserTokenRepository updateUserTokenRepository, IGetUserByTokenRepository getUserByTokenRepository, IGetUserTokensRepository getUserTokensRepository)
    {
        _updateUserTokenRepository = updateUserTokenRepository;
        _getUserByTokenRepository = getUserByTokenRepository;
        _getUserTokensRepository = getUserTokensRepository;
    }

    public async Task<Unit> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _getUserByTokenRepository.Get(request.Token, cancellationToken);
        if (user == null)
            return default;
        
        var tokens = await _getUserTokensRepository.Get(user.Id, cancellationToken);

        var refreshToken = tokens.Single(x => x.Token == request.Token);
            
        // if (!refreshToken.IsActive)
        //     return false;
            
        refreshToken.Revoked = DateTime.UtcNow;
        refreshToken.RevokedByIp = request.IpAddess;
        var updateUserToken = _mapper.Map<UserToken>(refreshToken);
        
        await _updateUserTokenRepository.Mutate(updateUserToken, cancellationToken);

        return default;
    }
}