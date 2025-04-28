using backend.DTO.LessonsDTO;
using backend.DTO.ReportsDTO;
using backend.Models;
using Microsoft.Identity.Client;

namespace backend.Interfaces.Services
{
    public interface ILessonsService
    {
        public Task<OperationResult> CreateTeacherLessonAsync(int teacherId, LessonCreateDTO lesson);
        public Task<OperationResult> CreateStudentLessonAsync(int studentId, int lessonId);

        public Task<IList<SubjectDTO>> GetAllSubjectsAsync();
        public Task<IList<LessonDTO>> GetUserLessonsAsync(int userId);
        public Task<IList<SearchUserDTO>> GetTeacherStudentsAsync(int teacherId);
        public Task<IList<SearchUserDTO>> GetStudentTeachersAsync(int studentId);

        public Task<OperationResult> UpdateTeacherLessonAsync(int userId, LessonEditDTO lesson);

        public Task<OperationResult> DeleteTeacherLessonAsync(int teacherId, int lessonId);
        public Task<OperationResult> DeleteStudentLessonAsync(int userId, int lessonId);
    }
}
