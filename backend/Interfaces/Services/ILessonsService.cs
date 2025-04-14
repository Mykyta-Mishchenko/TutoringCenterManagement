using backend.DTO.LessonsDTO;
using backend.Models;
using Microsoft.Identity.Client;

namespace backend.Interfaces.Services
{
    public interface ILessonsService
    {
        public Task<IList<SubjectDTO>> GetAllSubjectsAsync();
        public Task<IList<LessonDTO>> GetUserLessons(int userId);
        public Task<OperationResult> CreateTeacherLessonAsync(int userId, LessonCreateDTO lesson);
        public Task<OperationResult> DeleteTeacherLessonAsync(int teacherId, int lessonId);
        public Task<OperationResult> UpdateTeacherLessonAsync(int userId, LessonEditDTO lesson);
        public Task<OperationResult> CreateStudentLessonAsync(int userId, int lessonId);
        public Task<OperationResult> DeleteStudentLessonAsync(int userId, int lessonId);
    }
}
