using backend.Data.DataModels;

namespace JwtBackend.Interfaces
{
    public interface ITokenService
    {
        public int RefreshTokenExpirationDays { get; }
        public Task<string> CreateAccessToken(User user);
        public string CreateRefreshToken();
        public void AppendRefreshToken(HttpContext httpContext, string refreshToken);
    }
}