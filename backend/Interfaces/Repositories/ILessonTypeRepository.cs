using backend.Data.DataModels;

namespace backend.Interfaces.Repositories
{
    public interface ILessonTypeRepository
    {
        public Task<LessonType> CreateLessonType(LessonType lessonType);
        public Task<LessonType?> GetLessonType(LessonType lessonType);
    }
}
