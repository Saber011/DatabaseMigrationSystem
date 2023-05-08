using AutoMapper;
using DatabaseMigrationSystem.Common.Dto;
using DatabaseMigrationSystem.DataAccess.Entity;

namespace DatabaseMigrationSystem.UseCases.Migration.Mappings;

public class MigrationAutoMapperProfile: Profile
{
    public MigrationAutoMapperProfile()
    {
        CreateMap<TableInfo, TableInfoDto>()
            .ReverseMap();
    }
}