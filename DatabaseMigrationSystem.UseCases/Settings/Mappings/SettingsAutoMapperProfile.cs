using AutoMapper;
using DatabaseMigrationSystem.UseCases.Settings.Commands;

namespace DatabaseMigrationSystem.UseCases.Settings.Mappings;

public class SettingsAutoMapperProfile: Profile
{
    public SettingsAutoMapperProfile()
    {
        CreateMap<SetSettingsCommand, Infrastructure.DbContext.Entities.Settings>()
            .ReverseMap();
    }
}