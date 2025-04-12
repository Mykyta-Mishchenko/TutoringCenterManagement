using backend.Data.DataModels;
using backend.DTO.LessonsDTO;
using backend.Interfaces.Repositories;
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
        public async Task<IList<Subject>> GetAllSubjectsAsync()
        {
           return await _identityDbContext.Subjects.ToListAsync();
        }

        public async Task<IList<TeacherLesson>> GetTeacherLessons(int userId)
        {
            return await _identityDbContext.TeacherLessons
                .Where(l => l.TeacherId == userId)
                .Include(l => l.LessonType)
                    .ThenInclude(lt => lt.Subject)
                .Include(l => l.Schedule)
                .Include(l => l.StudentLessons).ToListAsync();
        }
        public async Task<IList<StudentLesson>> GetStudentLessons(int userId)
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
    }
}
