using backend.Data.DataModels;
using backend.DTO.LessonsDTO;
using backend.Models;

namespace backend.Interfaces.Repositories
{
    public interface ILessonsRepository
    {
        public Task<IList<Subject>> GetAllSubjectsAsync();

        public void DeleteTeacherLesson(TeacherLesson lesson);
        public Task<TeacherLesson?> GetTeacherLessonAsync(int lessonId);
        public Task<StudentLesson?> GetStudentLessonAsync(int studentId, int lessonId);
        public Task<IList<TeacherLesson>> GetTeacherLessonsAsync(int userId);
        public Task<IList<StudentLesson>> GetStudentLessonsAsync(int userId);
        public Task<TeacherLesson?> CreateTeacherLessonAsync(TeacherLesson lesson);
        public Task<TeacherLesson?> GetCrossingTeacherLessonAsync(TeacherLesson lesson);
        public Task<StudentLesson?> GetCrossingStudentLessonAsync(StudentLesson lesson);
        public Task<TeacherLesson?> UpdateTeacherLessonAsync(TeacherLesson updatedLesson);
        public Task<StudentLesson?> CreateStudentLessonAsync(StudentLesson lesson);
        public Task<OperationResult> DeleteStudentLessonAsync(StudentLesson lesson);
    }
}
