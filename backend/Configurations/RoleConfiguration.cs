using backend.Data.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jwtbackend.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.RoleId);

            builder.Property(r => r.RoleId).ValueGeneratedOnAdd();

            builder.Property(r => r.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(r => r.Name).IsUnique();

            builder.ToTable("Roles");
        }
    }
}
