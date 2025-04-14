using AutoMapper;
using backend.Data.DataModels;
using backend.DTO.LessonsDTO;
using backend.Interfaces.Repositories;
using backend.Interfaces.Services;
using backend.Models;
using backend.Repositories;

namespace backend.Services
{
    public class LessonsService : ILessonsService
    {
        private readonly ILessonsRepository _lessonsRepository;
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        private readonly ISubjectsRepository _subjectsRepository;
        private readonly IUsersService _usersService;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly ILessonTypeRepository _lessonTypeRepository;
        public LessonsService(
            ILessonsRepository lessonsRepository,
            IMapper mapper,
            IRoleRepository roleRepository,
            ISubjectsRepository subjectsRepository,
            IUsersService usersService,
            IScheduleRepository scheduleRepository,
            ILessonTypeRepository lessonTypeRepository
            )
        {
            _lessonsRepository = lessonsRepository;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _subjectsRepository = subjectsRepository;
            _usersService = usersService;
            _scheduleRepository = scheduleRepository;
            _lessonTypeRepository = lessonTypeRepository;
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
                var teacherLessons = await _lessonsRepository.GetTeacherLessonsAsync(userId);
                return _mapper.Map<IList<LessonDTO>>(teacherLessons);
            }
            else if(role == UserRole.student)
            {
                var studentLessons = await _lessonsRepository.GetStudentLessonsAsync(userId);
                var teacherLessons = studentLessons
                    .Select(l => l.TeacherLesson)
                    .Distinct()
                    .ToList();
                return _mapper.Map<IList<LessonDTO>>(teacherLessons);
            }

            return null;
        }

        public async Task<OperationResult> CreateTeacherLessonAsync(int userId, LessonCreateDTO lesson)
        {
            var (result, teacherLesson) = await BuildTeacherLessonAsync(lesson);

            if (result == OperationResult.Failure || teacherLesson == null)
            {
                return OperationResult.Failure;
            }

            var crossingLesson = await _lessonsRepository.GetCrossingTeacherLessonAsync(teacherLesson);

            if(crossingLesson != null)
            {
                return OperationResult.Failure;
            }

            var dbLesson = await _lessonsRepository.CreateTeacherLessonAsync(teacherLesson);

            if(dbLesson == null)
            {
                return OperationResult.Failure;
            }

            return OperationResult.Success;
        }

        public async Task<OperationResult> DeleteTeacherLessonAsync(int teacherId, int lessonId)
        {
            var lesson = await _lessonsRepository.GetTeacherLessonAsync(lessonId);

            if (lesson != null && lesson.TeacherId == teacherId)
            {
                _lessonsRepository.DeleteTeacherLesson(lesson);
                return OperationResult.Success;
            }

            return OperationResult.Failure;
        }

        public async Task<OperationResult> UpdateTeacherLessonAsync(int userId, LessonEditDTO lesson)
        {
            var (result, editedTeacherLesson) = await BuildTeacherLessonAsync(lesson);

            if (result == OperationResult.Failure || editedTeacherLesson == null)
            {
                return OperationResult.Failure;
            }

            editedTeacherLesson.LessonId = lesson.LessonId;

            var crossingLesson = await _lessonsRepository.GetCrossingTeacherLessonAsync(editedTeacherLesson);

            if(crossingLesson != null)
            {
                return OperationResult.Failure;
            }

            var dbLesson = await _lessonsRepository.GetTeacherLessonAsync(lesson.LessonId);

            if(dbLesson != null && (
                dbLesson.ScheduleId != editedTeacherLesson.ScheduleId ||
                dbLesson.TypeId != editedTeacherLesson.TypeId)
              )
            {
                editedTeacherLesson.LessonId = dbLesson.LessonId;
                dbLesson = await _lessonsRepository.UpdateTeacherLessonAsync(editedTeacherLesson);
            }

            if(dbLesson == null)
            {
                return OperationResult.Failure;
            }

            return OperationResult.Success;
        }

        private async Task<(OperationResult Result, TeacherLesson? Lesson)> BuildTeacherLessonAsync(LessonBaseDTO lessonDto)
        {
            var subject = await _subjectsRepository.GetSubject(lessonDto.SubjectId);
            var user = await _usersService.GetUser(lessonDto.UserId);

            if (subject == null || user == null)
            {
                return (OperationResult.Failure, null);
            }

            var lessonType = new LessonType()
            {
                SubjectId = subject.SubjectId,
                MaxStudentsCount = lessonDto.MaxStudentsCount,
                SchoolYear = lessonDto.SchoolYear,
                Price = lessonDto.Price
            };

            var dbLessonType = await _lessonTypeRepository.GetLessonType(lessonType)
                               ?? await _lessonTypeRepository.CreateLessonType(lessonType);

            var dbSchedule = await _scheduleRepository.GetSchedule(lessonDto.Day, lessonDto.Hour, lessonDto.Minutes)
                              ?? await _scheduleRepository.CreateSchedule(lessonDto.Day, lessonDto.Hour, lessonDto.Minutes);

            var teacherLesson = new TeacherLesson()
            {
                TeacherId = lessonDto.UserId,
                TypeId = dbLessonType.TypeId,
                ScheduleId = dbSchedule.DateId,
                Schedule = dbSchedule
            };

            return (OperationResult.Success, teacherLesson);
        }

        public  async Task<OperationResult> CreateStudentLessonAsync(int userId, int lessonId)
        {
            var dbUser = await _usersService.GetUser(userId);
            var dbStudentLesson = await _lessonsRepository.GetStudentLessonAsync(userId, lessonId);

            if(dbUser == null || dbStudentLesson != null)
            {
                return OperationResult.Failure;
            }

            var dbTeacherLesson = await _lessonsRepository.GetTeacherLessonAsync(lessonId);

            if(dbTeacherLesson == null)
            {
                return OperationResult.Failure;
            }

            var newStudentLesson = new StudentLesson()
            {
                StudentId = userId,
                LessonId = lessonId,
                TeacherLesson = dbTeacherLesson
            };

            var crossingLesson = await _lessonsRepository.GetCrossingStudentLessonAsync(newStudentLesson);

            if(crossingLesson != null)
            {
                return OperationResult.Failure;
            }

            dbStudentLesson = await _lessonsRepository.CreateStudentLessonAsync(newStudentLesson);

            if(dbStudentLesson == null)
            {
                return OperationResult.Failure;
            }

            return OperationResult.Success;
            
        }
        public async Task<OperationResult> DeleteStudentLessonAsync(int userId, int lessonId)
        {
            var dbUser = await _usersService.GetUser(userId);
            var dbStudentLesson = await _lessonsRepository.GetStudentLessonAsync(userId, lessonId);

            if(dbUser == null || dbStudentLesson == null)
            {
                return OperationResult.Failure;
            }

            return await _lessonsRepository.DeleteStudentLessonAsync(dbStudentLesson);
        }
    }
}
