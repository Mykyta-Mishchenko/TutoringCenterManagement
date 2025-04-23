using backend.Data.DataModels;
using backend.DTO.UsersDTO;
using backend.Interfaces.Repositories;
using backend.Interfaces.Services;
using backend.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace backend.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _usersRepository;
        private readonly IRoleRepository _rolesRepository;
        public UsersService(
            IUserRepository userRepository,
            IRoleRepository roleRepository
            )
        {
            _usersRepository = userRepository;
            _rolesRepository = roleRepository;
        }
        public async Task<UsersListDTO> GetUsersByFilterAsync(UsersFilterDTO filter)
        {
            return await _usersRepository.GetUsersByFilterAsync(filter);
        }

        public async Task<UserInfoDTO?> GetUser(int userId)
        {
            if(await IsUserTeacherAsync(userId))
            {
                return await _usersRepository.GetTeacherInfoAsync(userId);
            }
            else
            {
                return await _usersRepository.GetStudentInfoAsync(userId);
            }
        }

        public async Task<UserRole?> GetUserRole(int userId)
        {
            var userRoles = await _rolesRepository.GetUserRolesAsync(userId);
            var role =  userRoles.First(ur =>
                ur.Name == UserRole.teacher.ToString() ||
                ur.Name == UserRole.student.ToString());

            if (role == null)
                return null;

            return Enum.Parse<UserRole>(role.Name, ignoreCase: true);
        }

        private async Task<bool> IsUserTeacherAsync(int userId)
        {
            var userRoles = await _rolesRepository.GetUserRolesAsync(userId);
            var role = userRoles.FirstOrDefault(ur => ur.Name == UserRole.teacher.ToString());

            if (role == null)
                return false;

            return true;

        }
    }
}
