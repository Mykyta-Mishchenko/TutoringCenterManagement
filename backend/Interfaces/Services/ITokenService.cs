using backend.Data.DataModels;

namespace backend.Interfaces.Services
{
    public interface ITokenService
    {
        public int RefreshTokenExpirationDays { get; }
        public Task<string> CreateAccessToken(User user);
        public string CreateRefreshToken();
        public void AppendRefreshToken(HttpContext httpContext, string refreshToken);
    }
}