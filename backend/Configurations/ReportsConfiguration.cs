using backend.Data.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Configurations
{
    public class ReportsConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasKey(r => r.ReportId);

            builder.Property(r => r.ReportId).ValueGeneratedOnAdd();

            builder.Property(r => r.Description).HasMaxLength(500).IsRequired();

            builder.Property(r => r.DateTime).IsRequired();

            builder.HasOne(r => r.StudentLesson)
                .WithMany(sl => sl.Reports)
                .HasForeignKey(r => r.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Reports");
        }
    }
}
