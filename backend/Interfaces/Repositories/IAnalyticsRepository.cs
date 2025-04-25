using backend.DTO.AnalyticsDTO;

namespace backend.Interfaces.Repositories
{
    public interface IAnalyticsRepository
    {
        public Task<ICollection<SalaryReportDTO>> GetTeacherSalaryReports(SalaryFilterDTO filter);
        public Task<ICollection<SalaryReportDTO>> GetStudentPriceReports(SalaryFilterDTO filter);
        public Task<SalaryAnalyticsDTO> GetTeacherSalaryAnalyticsAsync(AnalyticsFilterDTO filter);
        public Task<SalaryAnalyticsDTO> GetStudentPriceAnalyticsAsync(AnalyticsFilterDTO filter);
        public Task<StudentAnalyticsDTO> GetTeacherMarksAnalyticsAsync(AnalyticsFilterDTO filter);
        public Task<StudentAnalyticsDTO> GetStudentMarksAnalyticsAsync(AnalyticsFilterDTO filter);
    }
}
