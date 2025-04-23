using backend.Data.DataModels;
using backend.Interfaces.Repositories;
using backend.Models;
using JwtBackend.Data;
using Microsoft.EntityFrameworkCore;

namespace JwtBackend.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly IdentityDbContext _identityDbContext;
        public SessionRepository (IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }
        public async Task<RefreshSession?> CreateSessionAsync(RefreshSession session)
        {
            await _identityDbContext.RefreshSessions.AddAsync(session);
            await _identityDbContext.SaveChangesAsync();
            return session;
        }

        public async Task<IList<RefreshSession>> GetUserSessionsAsync(User user)
        {
            var dbUser = await _identityDbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if(dbUser != null)
            {
                return await _identityDbContext.RefreshSessions
                    .Where(s => s.UserId == dbUser.UserId)
                    .ToListAsync();
            }
            return new List<RefreshSession> ();
        }

        public async Task<RefreshSession?> GetUserSessionByTokenAsync(string refreshToken)
        {
            return await _identityDbContext.RefreshSessions.FirstOrDefaultAsync(s => s.RefreshToken == refreshToken);
        }

        public async Task<OperationResult> DeleteSessionAsync(string refreshToken)
        {
            var session = await _identityDbContext.RefreshSessions.FirstOrDefaultAsync(s => s.RefreshToken == refreshToken);
            if(session != null)
            {
                _identityDbContext.RefreshSessions.Remove(session);
                await _identityDbContext.SaveChangesAsync();
                return OperationResult.Success;
            }

            return OperationResult.Failure;
        }

        public async Task<OperationResult> DeleteUserSessionsAsync(User user)
        {
            var dbUser = await _identityDbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if(dbUser != null)
            {
                var sessions =  await _identityDbContext.RefreshSessions.Where(s => s.UserId == dbUser.UserId).ToListAsync();
                _identityDbContext.RefreshSessions.RemoveRange(sessions);
                await _identityDbContext.SaveChangesAsync();
                return OperationResult.Success;
            }

            return OperationResult.Failure;
        }

        public async Task<RefreshSession?> UpdateSessionAsync(string oldRefreshToken, string newRefreshToken)
        {
            var session = await _identityDbContext.RefreshSessions.FirstOrDefaultAsync(s=>s.RefreshToken == oldRefreshToken);
            if(session != null)
            {
                session.RefreshToken = newRefreshToken;
                _identityDbContext.RefreshSessions.Update(session);
                await _identityDbContext.SaveChangesAsync();
                return session;
            }
            return null;
        }
    }
}
