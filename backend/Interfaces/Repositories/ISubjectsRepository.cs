using backend.Data.DataModels;
using backend.Models;

namespace backend.Interfaces.Repositories
{
    public interface ISubjectsRepository
    {
        public Task<Subject?> CreateSubjectAsync(string name);
        public Task<Subject?> GetSubjectAsync(int subjectId);
        public Task<Subject?> GetSubjectAsync(string name);
        public Task<OperationResult> DeleteSubjectAsync(int subjectId);
        public Task<OperationResult> DeleteSubjectAsync(string name);
    }
}
