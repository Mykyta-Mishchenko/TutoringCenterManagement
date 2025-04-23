using backend.Data.DataModels;
using backend.Models;

namespace backend.Interfaces.Repositories
{
    public interface IScheduleRepository
    {
        public Task<Schedule?> CreateScheduleAsync(int day, int hour, int minutes);

        public Task<Schedule?> GetScheduleAsync(int day, int hour, int minutes);

        public Task<OperationResult> DeleteScheduleAsync(Schedule schedule);
    }
}
