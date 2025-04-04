using backend.Data.DataModels;

namespace backend.Interfaces.Repositories
{
    public interface ISessionRepository
    {
        public Task AddSessionAsync(RefreshSession session);
        public void RemoveSession(string refreshToken);
        public void RemoveUserSessions(User user);
        /// <summary>
        /// Get session by User
        /// </summary>
        public Task<IList<RefreshSession>> GetSessionAsync(User user);
        /// <summary>
        /// Get session by Token
        /// </summary>
        public Task<RefreshSession> GetSessionAsync(string refreshToken);
        public Task UpdateSessionAsync(string oldRefreshToken, string newRefreshToken);
    }
}
