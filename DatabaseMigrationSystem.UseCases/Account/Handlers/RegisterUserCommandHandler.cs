using AutoMapper;
using DatabaseMigrationSystem.ApplicationServices.Interfaces.Account;
using DatabaseMigrationSystem.DataAccess.Interfaces.User;
using DatabaseMigrationSystem.Infrastructure.DbContext.Entities;
using DatabaseMigrationSystem.UseCases.Account.Commands;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DatabaseMigrationSystem.UseCases.Account.Handlers;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
{
    private readonly IMapper _mapper;
    private readonly IGetByLoginUserRepository _getByLoginUserRepository;
    private readonly ICreateUserRepository _createUserRepository;
    private readonly IGenerateRefreshTokenService _generateRefreshTokenService;
    private readonly ICreateUserTokenRepository _createUserTokenRepository;

    public RegisterUserCommandHandler(IMapper mapper, IGetByLoginUserRepository getByLoginUserRepository, ICreateUserRepository createUserRepository, IGenerateRefreshTokenService generateRefreshTokenService, ICreateUserTokenRepository createUserTokenRepository)
    {
        _mapper = mapper;
        _getByLoginUserRepository = getByLoginUserRepository;
        _createUserRepository = createUserRepository;
        _generateRefreshTokenService = generateRefreshTokenService;
        _createUserTokenRepository = createUserTokenRepository;
    }

    public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var hasher = new PasswordHasher<AuthenticateCommand>();

        var aut = _mapper.Map<AuthenticateCommand>(request);
        
        var passwordHash = hasher.HashPassword(aut, request.Password);

        var refreshToken = await _generateRefreshTokenService.Handle(request.IpAddess, cancellationToken);

        var user = await _getByLoginUserRepository.Get(request.Login, cancellationToken);

        if (user is not null)
        {
            throw new ValidationException("Данный пользователь уже был зарегестрирован");
        }

        var newUser = new User
        {
            Login = request.Login,
            Password = passwordHash,
        };

        var userToken = _mapper.Map<UserToken>(refreshToken);
        
        var userId = await _createUserRepository.Mutate(newUser, cancellationToken);

        userToken.UserId = userId;
        await _createUserTokenRepository.Mutate(userToken, cancellationToken);
        return default;
    }
}