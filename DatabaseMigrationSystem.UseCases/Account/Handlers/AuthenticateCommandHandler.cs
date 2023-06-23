using System.ComponentModel.DataAnnotations;
using Action.Platform.Common.Exceptions;
using AutoMapper;
using DatabaseMigrationSystem.ApplicationServices.Interfaces.Account;
using DatabaseMigrationSystem.Common.Dto;
using DatabaseMigrationSystem.DataAccess.Interfaces.User;
using DatabaseMigrationSystem.Infrastructure.DbContext.Entities;
using DatabaseMigrationSystem.UseCases.Account.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DatabaseMigrationSystem.UseCases.Account.Handlers;

public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, AuthenticateInfo>
{
    private readonly IMapper _mapper;
    private readonly IGetByLoginUserRepository _getByLoginRepository;
    private readonly IGetUserRolesRepository _getUserRolesRepository;
    private readonly IGenerateJwtTokenService _generateJwtTokenService;
    private readonly IGenerateRefreshTokenService _generateRefreshTokenService;
    private readonly ICreateUserTokenRepository _createUserTokenRepository;
    
    public AuthenticateCommandHandler(IMapper mapper, IGetByLoginUserRepository getByLoginRepository, IGetUserRolesRepository getUserRolesRepository, IGenerateJwtTokenService generateJwtTokenService, IGenerateRefreshTokenService generateRefreshTokenService, ICreateUserTokenRepository createUserTokenRepository)
    {
        _mapper = mapper;
        _getByLoginRepository = getByLoginRepository;
        _getUserRolesRepository = getUserRolesRepository;
        _generateJwtTokenService = generateJwtTokenService;
        _generateRefreshTokenService = generateRefreshTokenService;
        _createUserTokenRepository = createUserTokenRepository;
    }
    
    public async Task<AuthenticateInfo> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        var user = await _getByLoginRepository.Get(request.Login, cancellationToken);

        if (user is null)
        {
            throw new BrokenRulesException("Логин или пароль не верны", 1);
        }
        
        var hasher = new PasswordHasher<AuthenticateCommand>();

        if (hasher.VerifyHashedPassword(_mapper.Map<AuthenticateCommand>(user), user.Password, request.Password) == PasswordVerificationResult.Failed)
        {
            throw new BrokenRulesException("Логин или пароль не верны", 1);
        }
        var jwtToken = await _generateJwtTokenService.Handle(user.Id, cancellationToken);
        var refreshToken = await _generateRefreshTokenService.Handle(request.IpAddress, cancellationToken);
        refreshToken.Token = refreshToken.Token;

        var userToken = _mapper.Map<UserToken>(refreshToken);

        userToken.UserId = user.Id;
        await _createUserTokenRepository.Mutate(userToken, cancellationToken);
            
        return new AuthenticateInfo
        {
            Login = user.Login,
            Id = user.Id,
            JwtToken = jwtToken,
            RefreshToken = refreshToken.Token,
        };
    }
}