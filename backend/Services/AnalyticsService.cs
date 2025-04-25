using backend.DTO.AnalyticsDTO;
using backend.Interfaces.Repositories;
using backend.Interfaces.Services;
using backend.Models;

namespace backend.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IAnalyticsRepository _analyticsRepository;
        public AnalyticsService(IAnalyticsRepository analyticsRepository)
        {
            _analyticsRepository = analyticsRepository;
        }

        public async Task<StudentAnalyticsDTO?> GetMarksAnalyticsAsync(AnalyticsFilterDTO filter, UserRole role)
        {
            if (role == UserRole.teacher)
            {
                return await _analyticsRepository.GetTeacherMarksAnalyticsAsync(filter);
            }
            else if (role == UserRole.student)
            {
                return await _analyticsRepository.GetStudentMarksAnalyticsAsync(filter);
            }

            return new StudentAnalyticsDTO();
        }

        public async Task<SalaryAnalyticsDTO?> GetSalaryAnalyticsAsync(AnalyticsFilterDTO filter, UserRole role)
        {
            if (role == UserRole.teacher)
            {
                return await _analyticsRepository.GetTeacherSalaryAnalyticsAsync(filter);
            }
            else if (role == UserRole.student)
            {
                return await _analyticsRepository.GetStudentPriceAnalyticsAsync(filter);
            }

            return new SalaryAnalyticsDTO();
        }

        public async Task<ICollection<SalaryReportDTO>> GetSalaryReportsAsync(SalaryFilterDTO filter, UserRole role)
        {
            if(role == UserRole.teacher)
            {
                return await _analyticsRepository.GetTeacherSalaryReports(filter);
            }
            else if(role == UserRole.student)
            {
                return await _analyticsRepository.GetStudentPriceReports(filter);
            }

            return new List<SalaryReportDTO>();
        }
    }
}
