using backend.Data.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Configurations
{
    public class LessonTypesConfiguration : IEntityTypeConfiguration<LessonType>
    {
        public void Configure(EntityTypeBuilder<LessonType> builder)
        {
            builder.HasKey(lt => lt.TypeId);

            builder.Property(lt => lt.TypeId).ValueGeneratedOnAdd();

            builder.Property(lt => lt.SchoolYear).IsRequired();

            builder.Property(lt => lt.MaxStudentsCount).IsRequired();

            builder.Property(lt => lt.Price).IsRequired();

            builder.HasCheckConstraint("CK_Lesson_MaxStudentsCount", "[MaxStudentsCount] >= 1 AND [MaxStudentsCount] <= 5");

            builder.HasCheckConstraint("CK_Lesson_SchoolYear", "[SchoolYear] >= 1 AND [SchoolYear] <= 12");

            builder.HasCheckConstraint("CK_Lesson_Price", "[Price] >= 1 AND [Price] <= 5000");

            builder.HasOne(lt => lt.Subject)
                .WithMany(s => s.LessonTypes)
                .HasForeignKey(lt => lt.SubjectId);

            builder.HasMany(lt => lt.Lessons)
                .WithOne(l => l.LessonType)
                .HasForeignKey(l => l.TypeId);

            builder.ToTable("LessonTypes");
        }
    }
}
