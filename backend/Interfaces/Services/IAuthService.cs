using backend.Data.DataModels;
using backend.Models;
using JwtBackend.Models;
using Microsoft.Identity.Client;

namespace backend.Interfaces.Services
{
    public interface IAuthService
    {
        public Task<SignUpResult> SignUpAsync(User user, UserRole role);
        public Task<SignInResult> SignInAsync(User user);
        public Task<OperationResult> Logout(string refreshToken);
        /// <returns>Access token</returns>
        public Task<SessionTokens> RefreshSessionAsync(string refreshToken);
        public Task AddUserRoleAsync(User user, string roleName);
        public Task AddRoleAsync(string roleName);
    }
}
