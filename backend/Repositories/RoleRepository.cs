using backend.Data.DataModels;
using backend.Interfaces.Repositories;
using JwtBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace JwtBackend.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IdentityDbContext _identityDbContext;
        public RoleRepository(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }
        public async Task CreateRoleAsync(string roleName)
        {
            await _identityDbContext.Roles.AddAsync(new Role { Name = roleName});
            await _identityDbContext.SaveChangesAsync();
        }

        public void DeleteRole(string roleName)
        {
            var dbRole = _identityDbContext.Roles.FirstOrDefault(r => r.Name == roleName);
            if (dbRole != null)
            {
                _identityDbContext.Roles.Remove(dbRole);
                _identityDbContext.SaveChanges();
            }
        }

        public bool IsRoleExists(string roleName)
        {
            return _identityDbContext.Roles.FirstOrDefault(r => r.Name == roleName) != null;
        }

        public async Task<IList<Role>> GetRolesAsync()
        {
            return await _identityDbContext.Roles.ToListAsync();
        }

        public async Task<IList<Role>> GetUserRolesAsync(User user)
        {
            var dbUser = await _identityDbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if(dbUser != null)
            {
                return await _identityDbContext.UsersRoles
                .Where(ur => ur.UserId == dbUser.UserId)
                .Select(ur => ur.Role)
                .ToListAsync();
            }
            return new List<Role>();
        }

        public async Task GiveUserRoleAsync(User user, string roleName)
        {
            var dbRole = await _identityDbContext.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            var dbUser = await _identityDbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if(dbRole != null && dbUser != null)
            {
                var dbUserRole = await _identityDbContext.UsersRoles
                    .FirstOrDefaultAsync(ur => (ur.UserId == dbUser.UserId && ur.RoleId == dbRole.RoleId));
                if(dbUserRole == null)
                {
                    var userRole = new UserRoles() { UserId = dbUser.UserId, RoleId = dbRole.RoleId };
                    await _identityDbContext.UsersRoles.AddAsync(userRole);
                    await _identityDbContext.SaveChangesAsync();
                }
            }
        }

        public void RemoveUserRole(User user, string roleName)
        {
            var dbRole = _identityDbContext.Roles.FirstOrDefault(r => r.Name == roleName);
            var dbUser = _identityDbContext.Users.FirstOrDefault(u => u.Email == user.Email);
            if (dbRole != null && dbUser != null)
            {
                var dbUserRole = _identityDbContext.UsersRoles
                .FirstOrDefault(ur => (ur.UserId == dbUser.UserId && ur.RoleId == dbRole.RoleId));
                if (dbUserRole != null)
                {
                    _identityDbContext.UsersRoles.Remove(dbUserRole);
                    _identityDbContext.SaveChanges();
                }
            }
        }
    }
}
