using backend.Data.DataModels;

namespace backend.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task AddUser(User user);
        public Task RemoveUser(User user);
        public Task UpdateUser(User user);
        /// <summary>
        /// Get User by id
        /// </summary>
        public Task<User> GetUser(int Id);
        /// <summary>
        /// Get User by email
        /// </summary>
        public Task<User> GetUser(string email);
        public Task<string> GetUserProfile(int userId);
        public Task SetUserProfile(int userId, string profileImgUrl);
    }
}
