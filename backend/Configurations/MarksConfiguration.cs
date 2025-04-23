using backend.Data.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Configurations
{
    public class MarksConfiguration : IEntityTypeConfiguration<Mark>
    {
        public void Configure(EntityTypeBuilder<Mark> builder)
        {
            builder.HasKey(m => new { m.ReportId, m.MarkTypeId });

            builder.Property(m => m.Score).IsRequired();

            builder.HasCheckConstraint("CK_Marks_Score", "[Score] >= 1 AND [Score] <= 10");

            builder.HasOne(m => m.Report)
                .WithMany(r => r.Marks)
                .HasForeignKey(m => m.ReportId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.MarkType)
                .WithMany(mt => mt.Marks)
                .HasForeignKey(m => m.MarkTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Marks");
        }
    }
}
