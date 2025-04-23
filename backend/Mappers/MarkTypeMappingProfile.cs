using AutoMapper;
using backend.Data.DataModels;
using backend.DTO.ReportsDTO;
using System.Security;

namespace backend.Mappers
{
    public class MarkTypeMappingProfile : Profile
    {
        public MarkTypeMappingProfile() 
        {
            CreateMap<MarkType, MarkTypeDTO>();
        }
    }
}
