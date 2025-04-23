using backend.Data.DataModels;
using backend.Models;

namespace backend.Interfaces.Repositories
{
    public interface IMarkTypesRepository
    {
        public Task<MarkType> CreateMarkTypeAsync(MarkType markType);

        public Task<MarkType?> GetMarkTypeAsync(int id);
        public Task<MarkType?> GetMarkTypeAsync(string name);
        public Task<ICollection<MarkType>> GetAllMarkTypesAsync();

        public Task<OperationResult> DeleteMarkTypeAsync(int id);
        public Task<OperationResult> DeleteMarkTypeAsync(string name);
        public Task<OperationResult> DeleteMarkTypeAsync(MarkType markType);
    }
}
