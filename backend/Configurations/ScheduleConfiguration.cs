using backend.Data.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Configurations
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.HasKey(s => s.DateId);

            builder.Property(s => s.DateId).ValueGeneratedOnAdd();

            builder.Property(s => s.DayOfWeek).IsRequired();

            builder.Property(s => s.DayTime).IsRequired();

            builder.HasCheckConstraint("CK_Schedule_DayOfWeek", "[DayOfWeek] >= 1 AND [DayOfWeek] <= 7");

            builder.ToTable("Schedules");
        }
    }
}
