using AutoMapper;
using backend.Data.DataModels;
using backend.DTO.AuthDTO;

namespace JwtBackend.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<SignUpDTO, User>()
           .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
           .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.LastName) ? null : src.LastName))
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
           .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
           .ForMember(dest => dest.UserRoles, opt => opt.Ignore())
           .ForMember(dest => dest.Sessions, opt => opt.Ignore());

            CreateMap<SignInDTO, User>()
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
           .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
           .ForMember(dest => dest.UserRoles, opt => opt.Ignore())
           .ForMember(dest => dest.Sessions, opt => opt.Ignore());
        }
    }
}
