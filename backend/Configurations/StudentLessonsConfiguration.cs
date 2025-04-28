using backend.Data.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Configurations
{
    public class StudentLessonsConfiguration : IEntityTypeConfiguration<StudentLesson>
    {
        public void Configure(EntityTypeBuilder<StudentLesson> builder)
        {
            builder.HasKey(l => l.StudentLessonId);

            builder.Property(l => l.StudentLessonId).ValueGeneratedOnAdd();

            builder.HasIndex(l => l.StudentId);

            builder.HasOne(l => l.User)
                .WithMany(u => u.StudentLessons)
                .HasForeignKey(l => l.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(l => l.TeacherLesson)
                .WithMany(u => u.StudentLessons)
                .HasForeignKey(l => l.TeacherLessonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("StudentLessons");
        }
    }
}
