using AutoMapper;
using backend.Data.DataModels;
using backend.DTO.LessonsDTO;

namespace backend.Mappers
{
    public class LessonMappingProfile: Profile
    {
        public LessonMappingProfile() 
        {
            CreateMap<TeacherLesson, LessonDTO>()
                .ForMember(dest => dest.StudentsCount, opt => opt.MapFrom(src => src.StudentLessons.Count));

            CreateMap<LessonType, LessonTypeDTO>();
            CreateMap<Schedule, ScheduleDTO>()
                .ForMember(dest => dest.DayTime, opt => opt.MapFrom(src => DateTime.Today.Add(src.DayTime)));
            CreateMap<Subject, SubjectDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.SubjectName));
        }
    }
}
