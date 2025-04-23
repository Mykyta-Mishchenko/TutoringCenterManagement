using backend.Data.DataModels;
using backend.Interfaces.Repositories;
using backend.Models;
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
        public async Task<Subject> CreateSubjectAsync(string name)
        {
            var subject = new Subject { SubjectName = name };
            await _dbContext.Subjects.AddAsync(subject);
            await _dbContext.SaveChangesAsync();
            return subject;
        }

        public async Task<OperationResult> DeleteSubjectAsync(int subjectId)
        {
            var subject = await GetSubjectAsync(subjectId);
            if(subject != null)
            {
                _dbContext.Subjects.Remove(subject);
                await _dbContext.SaveChangesAsync();
                return OperationResult.Success;
            }

            return OperationResult.Failure;
        }

        public async Task<OperationResult> DeleteSubjectAsync(string name)
        {
            var subject = await GetSubjectAsync(name);
            if (subject != null)
            {
                _dbContext.Subjects.Remove(subject);
                await _dbContext.SaveChangesAsync();
                return OperationResult.Success;
            }

            return OperationResult.Failure;
        }

        public async Task<Subject?> GetSubjectAsync(int subjectId)
        {
            return await _dbContext.Subjects.FirstOrDefaultAsync(s => s.SubjectId == subjectId);
        }

        public async Task<Subject?> GetSubjectAsync(string name)
        {
            return await _dbContext.Subjects.FirstOrDefaultAsync(s => s.SubjectName == name);
        }
    }
}
