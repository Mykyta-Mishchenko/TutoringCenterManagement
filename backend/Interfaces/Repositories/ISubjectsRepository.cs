using backend.Data.DataModels;

namespace backend.Interfaces.Repositories
{
    public interface ISubjectsRepository
    {
        public Task CreatetSubject(string name);
        public Task<Subject?> GetSubject(int subjectId);
        public Task<Subject?> GetSubject(string name);
        public Task DeleteSubject(int subjectId);
        public Task DeleteSubject(string name);
    }
}
