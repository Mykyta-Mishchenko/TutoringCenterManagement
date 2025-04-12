using AutoMapper;
using backend.Data.DataModels;
using backend.DTO.LessonsDTO;
using backend.Interfaces.Repositories;
using backend.Interfaces.Services;
using backend.Models;

namespace backend.Services
{
    public class LessonsService : ILessonsService
    {
        private readonly ILessonsRepository _lessonsRepository;
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        public LessonsService(
            ILessonsRepository lessonsRepository,
            IMapper mapper,
            IRoleRepository roleRepository)
        {
            _lessonsRepository = lessonsRepository;
            _mapper = mapper;
            _roleRepository = roleRepository;
        }
        public async Task<IList<SubjectDTO>> GetAllSubjectsAsync()
        {
            var subjects = await _lessonsRepository.GetAllSubjectsAsync();
            return _mapper.Map<List<SubjectDTO>>(subjects);
        }
        public async Task<IList<LessonDTO>> GetUserLessons(int userId)
        {
            var userRoles = await _roleRepository.GetUserRolesAsync(userId);
            var entityRole = userRoles.Where(ur =>
                ur.Name == UserRole.teacher.ToString() ||
                ur.Name == UserRole.student.ToString()).First();

            if (entityRole == null)
                return null;

            var role =  Enum.Parse<UserRole>(entityRole.Name, ignoreCase: true);

            if (role == UserRole.teacher)
            {
                var teacherLessons = await _lessonsRepository.GetTeacherLessons(userId);
                return _mapper.Map<IList<LessonDTO>>(teacherLessons);
            }
            else if(role == UserRole.student)
            {
                var studentLessons = await _lessonsRepository.GetStudentLessons(userId);
                var teacherLessons = studentLessons
                    .Select(l => l.TeacherLesson)
                    .Distinct()
                    .ToList();
                return _mapper.Map<IList<LessonDTO>>(teacherLessons);
            }

            return null;
        }
    }
}
