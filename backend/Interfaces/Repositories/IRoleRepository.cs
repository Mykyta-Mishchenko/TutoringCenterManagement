using backend.Data.DataModels;

namespace backend.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        public Task CreateRole(string roleName);
        public void DeleteRole(string roleName);
        public bool IsRoleExists(string roleName);
        public Task GiveUserRole(User user, string roleName);
        public void RemoveUserRole(User user, string roleName);
        public Task<IList<Role>> GetUserRoles(User user);
        public Task<IList<Role>> GetRoles();
    }
}
