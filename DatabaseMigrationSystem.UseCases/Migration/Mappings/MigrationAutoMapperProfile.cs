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
            .ForMember(x => x.Fields, opt => opt.MapFrom(x => x.Columns))
            .ReverseMap();
        
        CreateMap<ColumnInfo, FieldDto>()
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.ColumnName))
            .ForMember(x => x.Type, opt => opt.MapFrom(x => x.DataType))
            .ReverseMap();
        
    }
}