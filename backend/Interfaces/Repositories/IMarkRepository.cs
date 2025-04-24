using backend.Data.DataModels;
using JwtBackend.Data;

namespace backend.Interfaces.Repositories
{
    public interface IMarkRepository
    {
        public Task<Mark?> CreateMarkAsync(Mark mark);
    }
}
