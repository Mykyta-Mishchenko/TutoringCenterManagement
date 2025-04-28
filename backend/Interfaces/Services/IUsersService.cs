using backend.Data.DataModels;
using backend.DTO.UsersDTO;
using backend.Models;

namespace backend.Interfaces.Services
{
    public interface IUsersService
    {
        public Task<UsersListDTO> GetUsersByFilterAsync(UsersFilterDTO filter);
        public Task<UserInfoDTO?> GetUser(int userId);
        public Task<User?> GetUserByEmail(string email);
        public Task<UserRole?> GetUserRole(int userId);
    }
}
