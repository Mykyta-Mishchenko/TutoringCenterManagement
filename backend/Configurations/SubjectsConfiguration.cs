using backend.Data.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Configurations
{
    public class SubjectsConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasKey(s => s.SubjectId);

            builder.Property(s => s.SubjectId).ValueGeneratedOnAdd();

            builder.Property(s => s.SubjectName).HasMaxLength(50).IsRequired();

            builder.HasMany(s => s.LessonTypes)
                .WithOne(l => l.Subject)
                .HasForeignKey(l => l.SubjectId);

            builder.ToTable("Subjects");
        }
    }
}
