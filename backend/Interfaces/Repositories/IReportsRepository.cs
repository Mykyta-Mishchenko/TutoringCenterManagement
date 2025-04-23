using backend.DTO.ReportsDTO;
using System.Runtime.CompilerServices;

namespace backend.Interfaces.Repositories
{
    public interface IReportsRepository
    {
        public Task<ReportsListDTO> GetStudentReportsByFilterAsync(ReportsFilterDTO filter);
        public Task<ReportsListDTO> GetTeacherReportsByFilterAsync(ReportsFilterDTO filter);
    }
}
