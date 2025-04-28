using backend.DTO.AnalyticsDTO;

namespace backend.Interfaces.Repositories
{
    public interface IAnalyticsRepository
    {
        public Task<ICollection<SalaryReportDTO>> GetTeacherSalaryReportsAsync(int teacherId);
        public Task<ICollection<SalaryReportDTO>> GetTeacherSalaryReportsAsync(SalaryFilterDTO filter);
        public Task<ICollection<SalaryReportDTO>> GetStudentPriceReportsAsync(SalaryFilterDTO filter);
        public Task<SalaryAnalyticsDTO> GetTeacherSalaryAnalyticsAsync(AnalyticsFilterDTO filter);
        public Task<SalaryAnalyticsDTO> GetStudentPriceAnalyticsAsync(AnalyticsFilterDTO filter);
        public Task<StudentAnalyticsDTO> GetTeacherMarksAnalyticsAsync(AnalyticsFilterDTO filter);
        public Task<StudentAnalyticsDTO> GetStudentMarksAnalyticsAsync(AnalyticsFilterDTO filter);
    }
}
