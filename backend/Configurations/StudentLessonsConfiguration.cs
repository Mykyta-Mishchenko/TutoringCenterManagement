using backend.Data.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Configurations
{
    public class StudentLessonsConfiguration : IEntityTypeConfiguration<StudentLesson>
    {
        public void Configure(EntityTypeBuilder<StudentLesson> builder)
        {
            builder.HasKey(l => new { l.LessonId, l.StudentId });

            builder.ToTable("StudentLessons");

            builder.HasOne(l => l.User)
                .WithMany(u => u.StudentLessons)
                .HasForeignKey(l => l.StudentId);

            builder.HasOne(l => l.TeacherLesson)
                .WithMany(u => u.StudentLessons)
                .HasForeignKey(l => l.LessonId);

            builder.ToTable("StudentLessons");
        }
    }
}
