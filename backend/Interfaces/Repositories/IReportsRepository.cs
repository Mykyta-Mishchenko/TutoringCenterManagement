using backend.Data.DataModels;
using backend.DTO.ReportsDTO;
using backend.Models;
using System.Runtime.CompilerServices;

namespace backend.Interfaces.Repositories
{
    public interface IReportsRepository
    {
        public Task<Report?> CreateReportAsync(Report report);

        public Task<Report?> GetReportByIdAsync(int reportId);
        public Task<ReportDTO?> GetReportDTOByIdAsync(int reportId);
        public Task<ICollection<ReportDTO>> GetTeacherReportsAsync(int teacherId);
        public Task<ReportsListDTO> GetStudentReportsByFilterAsync(ReportsFilterDTO filter);
        public Task<ReportsListDTO> GetTeacherReportsByFilterAsync(ReportsFilterDTO filter);
        public Task<IList<ReportScheduleDTO>> GetStudentMonthReportsAsync(int teacherId, int studentId);

        public Task<Report?> UpdateReportAsync(Report report);
    }
}
