using backend.Data.DataModels;
using backend.Interfaces.Repositories;
using backend.Interfaces.Services;
using backend.Models;
using JwtBackend.Models;
using System.Security.Cryptography;
using System.Text;

namespace JwtBackend.Services
{
    public class AuthService: IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly ITokenService _tokenService;

        public AuthService(
            IUserRepository userRepository, 
            IRoleRepository roleRepository,
            ISessionRepository sessionRepository,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _sessionRepository = sessionRepository;
            _tokenService = tokenService;
        }
        public async Task<SignUpResult> SignUp(User user, UserRole role)
        {
            var dbUser = await _userRepository.GetUser(user.Email);
            if(dbUser == null)
            {
                try {
                    user.Password = HashPassword(user.Password);
                    await _userRepository.AddUser(user);
                    dbUser = await _userRepository.GetUser(user.Email);

                    var roleName = role.ToString();

                    if (!_roleRepository.IsRoleExists(roleName))
                    {
                        await _roleRepository.CreateRole(roleName);
                    }
                    await _roleRepository.GiveUserRole(dbUser, roleName);

                    await _userRepository.SetUserProfile(dbUser.UserId, "");

                    return SignUpResult.Success;
                }
                catch (Exception ex)
                {
                    return SignUpResult.Failure;
                }
            }

            return SignUpResult.EmailFailure;
        }

        public async Task<SignInResult> SignIn(User user)
        {
            var dbUser = await _userRepository.GetUser(user.Email);
            if(dbUser != null)
            {
                if(dbUser.Password != HashPassword(user.Password))
                {
                    return SignInResult.Failure;
                }
                try
                {
                    var sessions = await _sessionRepository.GetSession(dbUser);
                    if (sessions.Count() > 5)
                    {
                        _sessionRepository.RemoveUserSessions(dbUser);
                    }
                    var session = new RefreshSession()
                    {
                        UserId = dbUser.UserId,
                        RefreshToken = _tokenService.CreateRefreshToken(),
                        ExpireTime = DateTime.UtcNow.AddDays(_tokenService.RefreshTokenExpirationDays)
                    };
                    await _sessionRepository.AddSession(session);
                    return SignInResult.SignedIn(
                        new SessionTokens()
                        {
                            RefreshToken = session.RefreshToken,
                            AccessToken = await _tokenService.CreateAccessToken(dbUser),
                        }
                    );
                }
                catch (Exception ex)
                {
                    return SignInResult.Failure;
                }
            }

            return SignInResult.NotAllowed;
        }

        public void Logout(string refreshToken)
        {
            _sessionRepository.RemoveSession(refreshToken);
        }

        public async Task AddUserRole(User user, string roleName)
        {
            var dbUser = await _userRepository.GetUser(user.Email);
            var roleExists = _roleRepository.IsRoleExists(roleName);
            if (dbUser != null && roleExists)
            {
                await _roleRepository.GiveUserRole(dbUser, roleName);
            }
        }

        public async Task AddRole(string roleName)
        {
            var roleExists = _roleRepository.IsRoleExists(roleName);
            if (!roleExists)
            {
                await _roleRepository.CreateRole(roleName);
            }
        }

        public async Task<SessionTokens?> RefreshSession(string refreshToken)
        {
            var session = await _sessionRepository.GetSession(refreshToken);

            if (session != null)
            {
                var user = await _userRepository.GetUser(session.UserId);

                var newRefreshToken = _tokenService.CreateRefreshToken();
                var newAccessToken = await _tokenService.CreateAccessToken(user);
                await _sessionRepository.UpdateSession(refreshToken, newRefreshToken);

                return new SessionTokens()
                {
                    RefreshToken = newRefreshToken,
                    AccessToken = newAccessToken,
                };
            }

            return null;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToHexString(hash);
            }
        }
    }
}
