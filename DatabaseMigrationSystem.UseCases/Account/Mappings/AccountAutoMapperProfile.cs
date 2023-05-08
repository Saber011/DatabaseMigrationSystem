using AutoMapper;
using DatabaseMigrationSystem.Common.Dto;
using DatabaseMigrationSystem.Infrastructure.DbContext.Entities;
using DatabaseMigrationSystem.UseCases.Account.Commands;

namespace DatabaseMigrationSystem.UseCases.Account.Mappings;

public class AccountAutoMapperProfile: Profile
{
    public AccountAutoMapperProfile()
    {
        CreateMap<AuthenticateCommand, RegisterUserCommand>()
            .ReverseMap();

        CreateMap<UserToken, RefreshToken>()
            .ReverseMap();
        
        CreateMap<AuthenticateCommand, User>()
            .ReverseMap();
        

    }
}