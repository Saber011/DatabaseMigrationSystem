using DatabaseMigrationSystem.Infrastructure.DbContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseMigrationSystem.Infrastructure.DbContext.EntityConfigurations;

public class MigrationLogConfiguration : IEntityTypeConfiguration<MigrationLog>
{
    public void Configure(EntityTypeBuilder<MigrationLog> builder)
    {
        builder.ToTable("migration_log");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Schema)
            .HasColumnName("schema")
            .HasComment("Схема");
        
        builder.Property(x => x.DataCount)
            .HasColumnName("dataCount")
            .HasComment("Текущие данны для вставки");
        
        builder.Property(x => x.TableName)
            .HasColumnName("tableName")
            .HasComment("Наименование таблицы");
    }
}