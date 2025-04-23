using AutoMapper;
using backend.DTO.ReportsDTO;
using backend.Interfaces.Repositories;
using backend.Interfaces.Services;
using backend.Models;

namespace backend.Services
{
    public class ReportsService : IReportsService
    {
        private readonly IMapper _mapper;
        private readonly IReportsRepository _reportsRepository;
        private readonly IMarkTypesRepository _markTypesRepository;
        public ReportsService(
            IMapper mapper,
            IReportsRepository reportsRepository,
            IMarkTypesRepository markTypesRepository)
        {
            _mapper = mapper;
            _reportsRepository = reportsRepository;
            _markTypesRepository = markTypesRepository;
        }

        public async Task<ReportsListDTO> GetReportsByFilterAsync(ReportsFilterDTO filter, UserRole role)
        {
            if (role == UserRole.student)
            {
                return await _reportsRepository.GetStudentReportsByFilterAsync(filter);
            }
            else if (role == UserRole.teacher)
            {
                return await _reportsRepository.GetTeacherReportsByFilterAsync(filter);
            }
            else
            {
                return new ReportsListDTO();
            }
        }

        public async Task<ICollection<MarkTypeDTO>> GetMarkTypesAsync()
        {
            var markTypes = await _markTypesRepository.GetAllMarkTypesAsync();
            return _mapper.Map<ICollection<MarkTypeDTO>>(markTypes);
        }
    }
}
