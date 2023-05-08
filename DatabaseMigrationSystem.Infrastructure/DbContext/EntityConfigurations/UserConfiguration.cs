using DatabaseMigrationSystem.Infrastructure.DbContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseMigrationSystem.Infrastructure.DbContext.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
        public void Configure(EntityTypeBuilder<User> builder)
        {
           builder.ToTable("users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasComment("Идентификатор пользователя");

            builder.Property(x => x.Login)
                .HasColumnName("login")
                .HasComment("Логин")
                .IsRequired();
            
        }
}