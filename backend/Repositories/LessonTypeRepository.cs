using backend.Data.DataModels;
using backend.Interfaces.Repositories;
using JwtBackend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class LessonTypeRepository : ILessonTypeRepository
    {
        private readonly IdentityDbContext _dbContext;
        public LessonTypeRepository(IdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        } 
        public async Task<LessonType> CreateLessonTypeAsync(LessonType lessonType)
        {
            await _dbContext.LessonTypes.AddAsync(lessonType);
            await _dbContext.SaveChangesAsync();
            return lessonType;
        }

        public async Task<LessonType?> GetLessonTypeAsync(LessonType lessonType)
        {
            return await _dbContext.LessonTypes
                .FirstOrDefaultAsync(
                    lt => lt.SubjectId == lessonType.SubjectId && 
                    lt.MaxStudentsCount == lessonType.MaxStudentsCount &&
                    lt.SchoolYear == lessonType.SchoolYear &&
                    lt.Price == lessonType.Price);
        }
    }
}
