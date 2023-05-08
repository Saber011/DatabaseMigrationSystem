using AutoMapper;
using DatabaseMigrationSystem.ApplicationServices.Interfaces.Account;
using DatabaseMigrationSystem.Common.Dto;
using DatabaseMigrationSystem.DataAccess.Interfaces.User;
using DatabaseMigrationSystem.Infrastructure.DbContext.Entities;
using DatabaseMigrationSystem.UseCases.Account.Commands;
using MediatR;

namespace DatabaseMigrationSystem.UseCases.Account.Handlers;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthenticateInfo>
{
    private readonly IMapper _mapper;
    private readonly IGenerateJwtTokenService _generateJwtTokenService;
    private readonly IGenerateRefreshTokenService _generateRefreshTokenService;
    private readonly IGetUserByTokenRepository _getUserByTokenRepository;
    private readonly IGetUserTokensRepository _getUserTokensRepository;
    private readonly IUpdateUserTokenRepository _updateUserTokenRepository;

    public RefreshTokenCommandHandler(IGenerateRefreshTokenService generateRefreshTokenService, IGenerateJwtTokenService generateJwtTokenService, IGetUserRolesRepository getUserRolesRepository, IMapper mapper, IGetUserByTokenRepository getUserByTokenRepository, IGetUserTokensRepository getUserTokensRepository, IUpdateUserTokenRepository updateUserTokenRepository)
    {
        _generateRefreshTokenService = generateRefreshTokenService;
        _generateJwtTokenService = generateJwtTokenService;
        _mapper = mapper;
        _getUserByTokenRepository = getUserByTokenRepository;
        _getUserTokensRepository = getUserTokensRepository;
        _updateUserTokenRepository = updateUserTokenRepository;
    }

    public async Task<AuthenticateInfo> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _getUserByTokenRepository.Get(request.Token, cancellationToken);
            
        if (user is null)
            return null;
        var userToken = await _getUserTokensRepository.Get(user.Id, cancellationToken);
            
        var refreshToken = userToken.Single(x => x.Token == request.Token);

        var jwtToken = await _generateJwtTokenService.Handle(user.Id, cancellationToken);
        var newRefreshToken = await _generateRefreshTokenService.Handle(request.IpAddess, cancellationToken);
        refreshToken.Revoked = DateTime.UtcNow;
        refreshToken.RevokedByIp = request.IpAddess;
        refreshToken.ReplacedByToken = newRefreshToken.Token;
        refreshToken.Token = jwtToken;
        var updateUserToken = _mapper.Map<UserToken>(refreshToken);
        
        await _updateUserTokenRepository.Mutate(updateUserToken, cancellationToken);
            
        return new AuthenticateInfo
        {
            Login = user.Login,
            Id = user.Id,
            JwtToken = jwtToken,
            RefreshToken = refreshToken.Token,
        };
    }
}