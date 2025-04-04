namespace backend.Interfaces.Services
{
    public interface IProfileService
    {
        public Task<string> SetUserProfileAsync(string refreshToken, IFormFile file);
        public Task<byte[]> GetUserProfileAsync(int userId);
    }
}
