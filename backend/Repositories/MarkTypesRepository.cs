using backend.Data.DataModels;
using backend.Interfaces.Repositories;
using backend.Models;
using JwtBackend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class MarkTypesRepository : IMarkTypesRepository
    {
        private readonly IdentityDbContext _dbContext;
        public MarkTypesRepository(IdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<MarkType> CreateMarkTypeAsync(MarkType markType)
        {
            await _dbContext.MarkTypes.AddAsync(markType);
            await _dbContext.SaveChangesAsync();
            return markType;
        }

        public async Task<OperationResult> DeleteMarkTypeAsync(int id)
        {
            var markType = await _dbContext.MarkTypes.FirstOrDefaultAsync(m => m.TypeId == id);
            if(markType != null)
            {
                _dbContext.Remove(markType);
                await _dbContext.SaveChangesAsync();
                return OperationResult.Success;
            }
            return OperationResult.Failure;
        }

        public async Task<OperationResult> DeleteMarkTypeAsync(string name)
        {
            var markType = await _dbContext.MarkTypes.FirstOrDefaultAsync(m => m.Name == name);
            if (markType != null)
            {
                _dbContext.Remove(markType);
                await _dbContext.SaveChangesAsync();
                return OperationResult.Success;
            }
            return OperationResult.Failure;
        }

        public async Task<OperationResult> DeleteMarkTypeAsync(MarkType markType)
        {
            _dbContext.Remove(markType);
            await _dbContext.SaveChangesAsync();
            return OperationResult.Success;
        }

        public async Task<ICollection<MarkType>> GetAllMarkTypesAsync()
        {
            return await _dbContext.MarkTypes.ToListAsync();
        }

        public async Task<MarkType?> GetMarkTypeAsync(int id)
        {
            return await _dbContext.MarkTypes.FirstOrDefaultAsync(m => m.TypeId == id);
        }

        public async Task<MarkType?> GetMarkTypeAsync(string name)
        {
            return await _dbContext.MarkTypes.FirstOrDefaultAsync(m => m.Name == name);
        }
    }
}
