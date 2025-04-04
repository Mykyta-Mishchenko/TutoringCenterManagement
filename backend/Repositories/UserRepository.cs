using backend.Data.DataModels;
using backend.Interfaces.Repositories;
using JwtBackend.Data;
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
        public async Task AddUserAsync(User user)
        {
            await _identityDbContext.Users.AddAsync(user);
            await _identityDbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserAsync(int Id)
        {
            return await _identityDbContext.Users.FindAsync(Id);
        }

        public async Task<User> GetUserAsync(string email)
        {
            return await _identityDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<string> GetUserProfileAsync(int userId)
        {
            var profile = await _identityDbContext.UsersProfile.FirstOrDefaultAsync(u => u.UserId == userId);
            return profile?.ProfileImgUrl ?? "";
        }

        public async Task SetUserProfileAsync(int userId, string profileImgUrl)
        {
            try
            {
                var dbUserProfile = await _identityDbContext.UsersProfile.FirstOrDefaultAsync(p => p.UserId == userId);
                var newUserProfile = new UserProfile
                {
                    UserId = userId,
                    ProfileImgUrl = profileImgUrl
                };

                if (dbUserProfile == null)
                {
                    await _identityDbContext.UsersProfile.AddAsync(newUserProfile);
                }
                else
                {
                    dbUserProfile.ProfileImgUrl = profileImgUrl;
                }

                await _identityDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task RemoveUserAsync(User user)
        {
            var dbUser = await _identityDbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if(dbUser != null)
            {
                _identityDbContext.Users.Remove(dbUser);
                await _identityDbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateUserAsync(User user)
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
