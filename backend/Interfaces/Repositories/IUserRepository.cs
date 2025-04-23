using backend.Data.DataModels;
using backend.DTO.UsersDTO;
using backend.Models;

namespace backend.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task<UserProfile?> CreateUserProfileAsync(int userId, string profileImgUrl);
        public Task<User?> CreateUserAsync(User user);

        public Task<User?> GetUserAsync(int Id);
        public Task<User?> GetUserAsync(string email);
        public Task<string> GetUserProfileAsync(int userId);
        public Task<UsersListDTO> GetUsersByFilterAsync(UsersFilterDTO filter);
        public Task<UserInfoDTO?> GetTeacherInfoAsync(int userId);
        public Task<UserInfoDTO?> GetStudentInfoAsync(int userId);

        public Task<User?> UpdateUserAsync(User user);

        public Task<OperationResult> DeleteUserAsync(User user);
        
    }
}
