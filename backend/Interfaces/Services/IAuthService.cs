using backend.Data.DataModels;
using backend.Models;
using JwtBackend.Models;
using Microsoft.Identity.Client;

namespace backend.Interfaces.Services
{
    public interface IAuthService
    {
        public Task<SignUpResult> SignUp(User user, UserRole role);
        public Task<SignInResult> SignIn(User user);
        public void Logout(string refreshToken);
        /// <returns>Access token</returns>
        public Task<SessionTokens> RefreshSession(string refreshToken);
        public Task AddUserRole(User user, string roleName);
        public Task AddRole(string roleName);
    }
}
