using backend.Data.DataModels;
using backend.DTO.LessonsDTO;
using backend.DTO.ReportsDTO;
using backend.Models;
using System.Runtime.CompilerServices;

namespace backend.Interfaces.Repositories
{
    public interface ILessonsRepository
    {
        public Task<TeacherLesson?> CreateTeacherLessonAsync(TeacherLesson lesson);
        public Task<StudentLesson?> CreateStudentLessonAsync(StudentLesson lesson);

        public Task<IList<Subject>> GetAllSubjectsAsync();
        public Task<TeacherLesson?> GetTeacherLessonAsync(int lessonId);
        public Task<StudentLesson?> GetStudentLessonAsync(int studentId, int lessonId);
        public Task<IList<TeacherLesson>> GetTeacherLessonsAsync(int userId);
        public Task<IList<StudentLesson>> GetStudentLessonsAsync(int userId);
        public Task<TeacherLesson?> GetCrossingTeacherLessonAsync(TeacherLesson lesson);
        public Task<StudentLesson?> GetCrossingStudentLessonAsync(StudentLesson lesson);
        public Task<IList<SearchUserDTO>> GetTeacherStudentsAsync(int teacherId);
        public Task<IList<SearchUserDTO>> GetStudentTeachersAsync(int studentId);

        public Task<TeacherLesson?> UpdateTeacherLessonAsync(TeacherLesson updatedLesson);

        public Task<OperationResult> DeleteTeacherLessonAsync(TeacherLesson lesson);
        public Task<OperationResult> DeleteStudentLessonAsync(StudentLesson lesson);
    }
}
