using backend.Data.DataModels;
using backend.DTO.LessonsDTO;
using backend.DTO.ReportsDTO;
using backend.Interfaces.Repositories;
using backend.Models;
using JwtBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace backend.Repositories
{
    public class LessonsRepository : ILessonsRepository
    {
        private IdentityDbContext _identityDbContext;
        public LessonsRepository(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<OperationResult> DeleteTeacherLessonAsync(TeacherLesson lesson)
        {
            _identityDbContext.TeacherLessons.Remove(lesson);
            await _identityDbContext.SaveChangesAsync();
            return OperationResult.Success;
        }

        public async Task<TeacherLesson?> GetTeacherLessonAsync(int lessonId)
        {
            return await _identityDbContext.TeacherLessons.FirstOrDefaultAsync(l => l.LessonId == lessonId);
        }

        public async Task<StudentLesson?> GetStudentLessonAsync(int studentId, int teacherLessonId)
        {
            return await _identityDbContext.StudentLessons
                .FirstOrDefaultAsync(l => l.LessonId == teacherLessonId && l.StudentId == studentId);
        }

        public async Task<IList<Subject>> GetAllSubjectsAsync()
        {
           return await _identityDbContext.Subjects.ToListAsync();
        }

        public async Task<IList<TeacherLesson>> GetTeacherLessonsAsync(int userId)
        {
            return await _identityDbContext.TeacherLessons
                .Where(l => l.TeacherId == userId)
                .Include(l => l.LessonType)
                    .ThenInclude(lt => lt.Subject)
                .Include(l => l.Schedule)
                .Include(l => l.StudentLessons).ToListAsync();
        }
        public async Task<IList<StudentLesson>> GetStudentLessonsAsync(int userId)
        {
            return await _identityDbContext.StudentLessons
                .Where(sl => sl.StudentId == userId)
                .Include(sl => sl.TeacherLesson)
                    .ThenInclude(tl => tl.Schedule)
                .Include(sl => sl.TeacherLesson)
                    .ThenInclude(tl => tl.LessonType)
                    .ThenInclude(lt => lt.Subject)
                .Include(sl => sl.TeacherLesson)
                    .ThenInclude(tl => tl.StudentLessons).ToListAsync();
        }

        public async Task<TeacherLesson?> CreateTeacherLessonAsync(TeacherLesson lesson)
        {
            await _identityDbContext.TeacherLessons.AddAsync(lesson);
            await _identityDbContext.SaveChangesAsync();
            return lesson;
        }

        public async Task<TeacherLesson?> GetCrossingTeacherLessonAsync(TeacherLesson lesson)
        {
            var oneHour = new TimeSpan(1, 0, 0);
            var lessonStart = lesson.Schedule.DayTime;
            var lessonEnd = lessonStart.Add(oneHour);

            var lessons = await _identityDbContext.TeacherLessons
                .Include(l => l.Schedule)
                .Where(l => l.TeacherId == lesson.TeacherId &&
                            l.Schedule.DayOfWeek == lesson.Schedule.DayOfWeek &&
                            l.LessonId != lesson.LessonId)
                .ToListAsync();

            return lessons.FirstOrDefault(l =>
                l.Schedule.DayTime < lessonEnd &&
                l.Schedule.DayTime.Add(oneHour) > lessonStart);
        }

        public async Task<StudentLesson?> GetCrossingStudentLessonAsync(StudentLesson lesson)
        {
            var schedule = await _identityDbContext.Schedules
                .FirstOrDefaultAsync(s => s.DateId == lesson.TeacherLesson.ScheduleId);
            var oneHour = new TimeSpan(1, 0, 0);
            var lessonStart = schedule.DayTime;
            var lessonEnd = lessonStart.Add(oneHour);

            var lessons = await _identityDbContext.StudentLessons
                .Include(l => l.TeacherLesson.Schedule)
                .Where(l => l.StudentId == lesson.StudentId &&
                            l.TeacherLesson.Schedule.DayOfWeek == schedule.DayOfWeek &&
                            l.LessonId != lesson.LessonId)
                .ToListAsync();

            return lessons.FirstOrDefault(l =>
                l.TeacherLesson.Schedule.DayTime < lessonEnd &&
                l.TeacherLesson.Schedule.DayTime.Add(oneHour) > lessonStart);
        }

        public async Task<TeacherLesson?> UpdateTeacherLessonAsync(TeacherLesson updatedLesson)
        {
            var dbLesson = await _identityDbContext.TeacherLessons.Include(l=>l.LessonType)
                .FirstOrDefaultAsync(l => l.LessonId == updatedLesson.LessonId);

            if (dbLesson == null) return null;

            var newLessonType = await _identityDbContext.LessonTypes
                .FirstOrDefaultAsync(lt => lt.TypeId == updatedLesson.TypeId);

            var oldLessonCount = dbLesson.LessonType.MaxStudentsCount; 

            if(newLessonType?.MaxStudentsCount < oldLessonCount)
            {
                var allSubscribedStudents = await _identityDbContext.StudentLessons
                    .Where(l => l.LessonId == dbLesson.LessonId).ToListAsync();

                if(allSubscribedStudents.Count > newLessonType?.MaxStudentsCount)
                {
                    _identityDbContext.StudentLessons.RemoveRange(allSubscribedStudents);
                }
            }

            dbLesson.TypeId = updatedLesson.TypeId;
            dbLesson.ScheduleId = updatedLesson.ScheduleId;

            _identityDbContext.TeacherLessons.Update(dbLesson);
            await _identityDbContext.SaveChangesAsync();
            return dbLesson;
        }

        public async Task<StudentLesson?> CreateStudentLessonAsync(StudentLesson lesson)
        {
            await _identityDbContext.StudentLessons.AddAsync(lesson);
            await _identityDbContext.SaveChangesAsync();
            return lesson;
        }

        public async Task<OperationResult> DeleteStudentLessonAsync(StudentLesson lesson)
        {
            _identityDbContext.StudentLessons.Remove(lesson);
            await _identityDbContext.SaveChangesAsync();
            return OperationResult.Success;
        }

        public async Task<IList<SearchUserDTO>> GetTeacherStudentsAsync(int teacherId)
        {
            return await _identityDbContext.StudentLessons
                .Where(l => l.TeacherLesson.TeacherId == teacherId)
                .Select(l => new SearchUserDTO
                {
                    UserId = l.User.UserId,
                    FullName = $"{l.User.FirstName} {l.User.LastName}"
                })
                .Distinct().ToListAsync();
        }
        public async Task<IList<SearchUserDTO>> GetStudentTeachersAsync(int studentId)
        {
            return await _identityDbContext.StudentLessons
                .Where(l => l.StudentId == studentId)
                .Select(l => new SearchUserDTO
                {
                    UserId = l.TeacherLesson.User.UserId,
                    FullName = $"{l.TeacherLesson.User.FirstName} {l.TeacherLesson.User.LastName}"
                })
                .Distinct().ToListAsync();
        }

        public async Task<IList<TeacherLesson>> GetTeacherLessonsByStudentAsync(int teacherId, int studentId)
        {
            return await _identityDbContext.TeacherLessons
                .Where(l => l.TeacherId == teacherId && l.StudentLessons.Any(l => l.StudentId == studentId))
                .Include(l => l.Schedule)
                .ToListAsync();
        }
    }
}
