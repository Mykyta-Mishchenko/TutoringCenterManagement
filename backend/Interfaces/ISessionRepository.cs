using backend.Data.DataModels;

namespace JwtBackend.Interfaces
{
    public interface ISessionRepository
    {
        public Task AddSession(RefreshSession session);
        public void RemoveSession(string refreshToken);
        public void RemoveUserSessions(User user);
        /// <summary>
        /// Get session by User
        /// </summary>
        public Task<IList<RefreshSession>> GetSession(User user);
        /// <summary>
        /// Get session by Token
        /// </summary>
        public Task<RefreshSession> GetSession(string refreshToken);
        public Task UpdateSession(string oldRefreshToken, string newRefreshToken); 
    }
}
