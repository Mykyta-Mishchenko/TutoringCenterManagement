using backend.Data.DataModels;
using backend.Interfaces.Repositories;
using JwtBackend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class SubjectsRepository : ISubjectsRepository
    {
        private readonly IdentityDbContext _dbContext;

        public SubjectsRepository(IdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreatetSubject(string name)
        {
            await _dbContext.Subjects.AddAsync(new Subject { SubjectName = name });
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteSubject(int subjectId)
        {
            var subject = await GetSubject(subjectId);
            _dbContext.Subjects.Remove(subject);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteSubject(string name)
        {
            var subject = await GetSubject(name);
            _dbContext.Subjects.Remove(subject);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Subject?> GetSubject(int subjectId)
        {
            return await _dbContext.Subjects.FirstOrDefaultAsync(s => s.SubjectId == subjectId);
        }

        public async Task<Subject?> GetSubject(string name)
        {
            return await _dbContext.Subjects.FirstOrDefaultAsync(s => s.SubjectName == name);
        }
    }
}
