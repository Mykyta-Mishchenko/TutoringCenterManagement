using backend.DTO.AnalyticsDTO;
using backend.Models;

namespace backend.Interfaces.Services
{
    public interface IAnalyticsService
    {
        public Task<ICollection<SalaryReportDTO>> GetSalaryReportsAsync(SalaryFilterDTO filter, UserRole role);
        public Task<SalaryAnalyticsDTO?> GetSalaryAnalyticsAsync(AnalyticsFilterDTO filter, UserRole role);
        public Task<StudentAnalyticsDTO?> GetMarksAnalyticsAsync(AnalyticsFilterDTO filter, UserRole role);
    }
}
