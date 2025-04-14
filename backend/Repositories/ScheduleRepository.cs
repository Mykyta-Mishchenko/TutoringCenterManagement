using backend.Data.DataModels;
using backend.Interfaces.Repositories;
using JwtBackend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly IdentityDbContext _dbContext;
        public ScheduleRepository(IdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Schedule> CreateSchedule(int day, int hour, int minutes)
        {
            var schedule = new Schedule() { DayOfWeek = day, DayTime = new TimeSpan(hour, minutes, 0) };
            await _dbContext.Schedules.AddAsync(schedule);
            await _dbContext.SaveChangesAsync();
            return schedule;
        }

        public void DeleteSchedule(Schedule schedule)
        {
            _dbContext.Schedules.Remove(schedule);
            _dbContext.SaveChanges();
        }

        public async Task<Schedule?> GetSchedule(int day, int hour, int minutes)
        {
            return await _dbContext.Schedules
                .FirstOrDefaultAsync(s => s.DayOfWeek == day && s.DayTime == new TimeSpan(hour, minutes, 0));
        }
    }
}
