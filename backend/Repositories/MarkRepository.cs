using backend.Data.DataModels;
using backend.Interfaces.Repositories;
using JwtBackend.Data;

namespace backend.Repositories
{
    public class MarkRepository : IMarkRepository
    {
        private readonly IdentityDbContext _dbContext;
        public MarkRepository(IdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Mark?> CreateMarkAsync(Mark mark)
        {
            await _dbContext.Marks.AddAsync(mark);
            await _dbContext.SaveChangesAsync();
            return mark;
        }
    }
}
