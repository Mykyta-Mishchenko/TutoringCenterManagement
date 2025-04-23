using backend.DTO.ReportsDTO;
using backend.Models;

namespace backend.Interfaces.Services
{
    public interface IReportsService
    {
        public Task<ReportsListDTO> GetReportsByFilterAsync(ReportsFilterDTO filter, UserRole role);
        public Task<ICollection<MarkTypeDTO>> GetMarkTypesAsync();
    }
}
