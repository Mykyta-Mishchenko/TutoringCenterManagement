using backend.Data.DataModels;

namespace backend.Interfaces.Repositories
{
    public interface ISessionRepository
    {
        public Task AddSessionAsync(RefreshSession session);
        public void RemoveSession(string refreshToken);
        public void RemoveUserSessions(User user);
        public Task<IList<RefreshSession>> GetUserSessionsAsync(User user);
        public Task<RefreshSession?> GetUserSessionByTokenAsync(string refreshToken);
        public Task UpdateSessionAsync(string oldRefreshToken, string newRefreshToken);
    }
}
