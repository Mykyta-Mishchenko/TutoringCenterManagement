using backend.Data.DataModels;

namespace backend.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        public Task CreateRoleAsync(string roleName);
        public void DeleteRole(string roleName);
        public bool IsRoleExists(string roleName);
        public Task GiveUserRoleAsync(User user, string roleName);
        public void RemoveUserRole(User user, string roleName);
        public Task<IList<Role>> GetUserRolesAsync(User user);
        public Task<IList<Role>> GetUserRolesAsync(int userId);
        public Task<IList<Role>> GetRolesAsync();
    }
}
