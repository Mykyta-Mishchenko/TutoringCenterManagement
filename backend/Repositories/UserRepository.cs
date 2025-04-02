using backend.Data.DataModels;
using JwtBackend.Data;
using JwtBackend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JwtBackend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityDbContext _identityDbContext;
        public UserRepository(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }
        public async Task AddUser(User user)
        {
            await _identityDbContext.Users.AddAsync(user);
            await _identityDbContext.SaveChangesAsync();
        }

        public async Task<User> GetUser(int Id)
        {
            return await _identityDbContext.Users.FindAsync(Id);
        }

        public async Task<User> GetUser(string email)
        {
            return await _identityDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task RemoveUser(User user)
        {
            var dbUser = await _identityDbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if(dbUser != null)
            {
                _identityDbContext.Users.Remove(dbUser);
                await _identityDbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateUser(User user)
        {
            var dbUser = await _identityDbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if(dbUser != null)
            {
                user.UserId = dbUser.UserId;
                _identityDbContext.Users.Update(user);
                await _identityDbContext.SaveChangesAsync();
            }
        }
    }
}
