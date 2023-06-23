using AutoMapper;
using DatabaseMigrationSystem.Common.Dto;

namespace DatabaseMigrationSystem.UseCases.User.Mappings;

public class UserAutoMapperProfile: Profile
{
    public UserAutoMapperProfile()
    {
        CreateMap<Infrastructure.DbContext.Entities.User, UserDto>()
            .ReverseMap();
    }
}