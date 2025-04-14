using backend.Data.DataModels;
using backend.DTO.UsersDTO;

namespace backend.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task AddUserAsync(User user);
        public Task RemoveUserAsync(User user);
        public Task UpdateUserAsync(User user);
        public Task<User?> GetUserAsync(int Id);
        public Task<User?> GetUserAsync(string email);
        public Task<string> GetUserProfileAsync(int userId);
        public Task SetUserProfileAsync(int userId, string profileImgUrl);
        public Task<UsersListDTO> GetUsersByFilterAsync(UsersFilterDTO filter);
        public Task<UserInfoDTO?> GetTeacherInfoAsync(int userId);
        public Task<UserInfoDTO?> GetStudentInfoAsync(int userId);
    }
}
