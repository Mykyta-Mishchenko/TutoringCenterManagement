using backend.Data.DataModels;

namespace backend.Interfaces.Repositories
{
    public interface ILessonTypeRepository
    {
        public Task<LessonType> CreateLessonTypeAsync(LessonType lessonType);

        public Task<LessonType?> GetLessonTypeAsync(LessonType lessonType);
    }
}
