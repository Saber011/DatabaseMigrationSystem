using DatabaseMigrationSystem.Infrastructure.DbContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseMigrationSystem.Infrastructure.DbContext.EntityConfigurations;

public class SettingsConfiguration : IEntityTypeConfiguration<Settings>
{
    public void Configure(EntityTypeBuilder<Settings> builder)
    {
        builder.ToTable("settings");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.DestinationConnectionString)
            .HasColumnName("destination_connection_string")
            .HasComment("Строка подключения к базе данных");
        
        builder.Property(x => x.DestinationDatabaseType)
            .HasColumnName("destination_database_type")
            .HasComment("Тип базы данных");
        
        builder.Property(x => x.SourceConnectionString)
            .HasColumnName("source_connection_string")
            .HasComment("Строка подключения к базе данных");
        
        builder.Property(x => x.SourceDatabaseType)
            .HasColumnName("source_database_type")
            .HasComment("Тип базы данных");

        builder.Property(x => x.UserId)
            .HasColumnName("user_id");

    }
}