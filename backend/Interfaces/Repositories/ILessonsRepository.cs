using backend.Data.DataModels;
using backend.DTO.LessonsDTO;

namespace backend.Interfaces.Repositories
{
    public interface ILessonsRepository
    {
        public Task<IList<Subject>> GetAllSubjectsAsync();
        public Task<IList<TeacherLesson>> GetTeacherLessons(int userId);
        public Task<IList<StudentLesson>> GetStudentLessons(int userId);
    }
}
