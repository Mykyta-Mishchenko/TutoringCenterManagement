using backend.Data.DataModels;

namespace backend.Interfaces.Repositories
{
    public interface IScheduleRepository
    {
        public Task<Schedule> CreateSchedule(int day, int hour, int minutes);
        public Task<Schedule?> GetSchedule(int day, int hour, int minutes);
        public void DeleteSchedule(Schedule schedule);
    }
}
