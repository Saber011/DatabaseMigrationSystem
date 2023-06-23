using AutoMapper;
using DatabaseMigrationSystem.Common.Dto;
using DatabaseMigrationSystem.DataAccess.Entity;

namespace DatabaseMigrationSystem.UseCases.Migration.Mappings;

public class MigrationAutoMapperProfile: Profile
{
    public MigrationAutoMapperProfile()
    {
        CreateMap<TableInfo, TableInfoDto>()
            .ForMember(x => x.DataCount, opt => opt.MapFrom(x => x.RowCount))
            .ReverseMap();
    }
}