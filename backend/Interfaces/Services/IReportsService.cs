using backend.DTO.ReportsDTO;
using backend.Models;

namespace backend.Interfaces.Services
{
    public interface IReportsService
    {
        public Task<OperationResult> CreateReport(ReportCreatingDTO report);

        public Task<ICollection<ReportDTO>> GetTeacherReportsAsync(int teacherId);
        public Task<ReportDTO?> GetReportByIdAsync(int reportId);
        public Task<ReportsListDTO> GetReportsByFilterAsync(ReportsFilterDTO filter, UserRole role);
        public Task<ICollection<MarkTypeDTO>> GetMarkTypesAsync();
        public Task<ICollection<ReportScheduleDTO>> GetUnassignedReportsAsync(int teacherId, int studentId);

        public Task<OperationResult> UpdateReportAsync(ReportEditingDTO report);
    }
}
