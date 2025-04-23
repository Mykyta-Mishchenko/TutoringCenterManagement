using backend.Data.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Configurations
{
    public class MarkTypesConfiguration : IEntityTypeConfiguration<MarkType>
    {
        public void Configure(EntityTypeBuilder<MarkType> builder)
        {
            builder.HasKey(mt => mt.TypeId);

            builder.Property(mt => mt.TypeId).ValueGeneratedOnAdd();

            builder.Property(mt => mt.Name).IsRequired();

            builder.ToTable("MarksTypes");
        }
    }
}
