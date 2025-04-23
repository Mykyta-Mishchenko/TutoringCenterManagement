using backend.Data.DataModels;
using backend.Models;

namespace backend.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        public Task<Role?> CreateRoleAsync(string roleName);
        public Task<UserRoles?> CreateUserRoleAsync(User user, string roleName);

        public Task<IList<Role>> GetUserRolesAsync(User user);
        public Task<IList<Role>> GetUserRolesAsync(int userId);
        public Task<IList<Role>> GetRolesAsync();

        public Task<bool> IsRoleExistsAsync(string roleName);

        public Task<OperationResult> DeleteRoleAsync(string roleName);
        public Task<OperationResult> DeleteUserRoleAsync(User user, string roleName);
        
    }
}
