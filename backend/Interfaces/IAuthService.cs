using backend.Data.DataModels;
using JwtBackend.Models;
using Microsoft.Identity.Client;

namespace JwtBackend.Interfaces
{
    public interface IAuthService
    {
        public Task<SignUpResult> SignUp(User user);
        public Task<SignInResult> SignIn(User user);
        public void Logout(string refreshToken);
        /// <returns>Access token</returns>
        public Task<SessionTokens> RefreshSession(string refreshToken);
        public Task AddUserRole(User user, string roleName);
        public Task AddRole(string roleName); 
    }
}
