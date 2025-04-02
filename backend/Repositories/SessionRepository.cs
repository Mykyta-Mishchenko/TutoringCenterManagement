using backend.Data.DataModels;
using JwtBackend.Data;
using JwtBackend.Interfaces;
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
        public async Task AddSession(RefreshSession session)
        {
            await _identityDbContext.RefreshSessions.AddAsync(session);
            await _identityDbContext.SaveChangesAsync();
        }

        public async Task<IList<RefreshSession>> GetSession(User user)
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

        public async Task<RefreshSession> GetSession(string refreshToken)
        {
            return await _identityDbContext.RefreshSessions.FirstOrDefaultAsync(s => s.RefreshToken == refreshToken);
        }

        public void RemoveSession(string refreshToken)
        {
            var session = _identityDbContext.RefreshSessions.FirstOrDefault(s => s.RefreshToken == refreshToken);
            if(session != null)
            {
                _identityDbContext.RefreshSessions.Remove(session);
                _identityDbContext.SaveChanges();
            }
        }

        public void RemoveUserSessions(User user)
        {
            var dbUser = _identityDbContext.Users.FirstOrDefault(u => u.Email == user.Email);
            if(dbUser != null)
            {
                var sessions =  _identityDbContext.RefreshSessions.Where(s => s.UserId == dbUser.UserId).ToList();
                _identityDbContext.RefreshSessions.RemoveRange(sessions);
                _identityDbContext.SaveChanges();
            }
        }

        public async Task UpdateSession(string oldRefreshToken, string newRefreshToken)
        {
            var session = await _identityDbContext.RefreshSessions.FirstOrDefaultAsync(s=>s.RefreshToken == oldRefreshToken);
            if(session != null)
            {
                session.RefreshToken = newRefreshToken;
                _identityDbContext.RefreshSessions.Update(session);
                await _identityDbContext.SaveChangesAsync();
            }
        }
    }
}
