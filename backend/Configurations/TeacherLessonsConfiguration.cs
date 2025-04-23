using backend.Data.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Configurations
{
    public class TeacherLessonsConfiguration : IEntityTypeConfiguration<TeacherLesson>
    {
        public void Configure(EntityTypeBuilder<TeacherLesson> builder)
        {
            builder.HasKey(l => l.LessonId);

            builder.Property(l => l.LessonId).ValueGeneratedOnAdd();

            builder.HasOne(l => l.User)
                .WithMany(u => u.TeacherLessons)
                .HasForeignKey(l => l.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(l => l.LessonType)
                .WithMany(u => u.Lessons)
                .HasForeignKey(l => l.TypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(l => l.Schedule)
                .WithMany(u => u.Lessons)
                .HasForeignKey(l => l.ScheduleId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("TeacherLessons");
        }
    }
}
