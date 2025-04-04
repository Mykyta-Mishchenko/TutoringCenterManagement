namespace backend.Interfaces.Services
{
    public interface IProfileService
    {
        public Task<string> SetUserProfile(string refreshToken, IFormFile file);
        public Task<byte[]> GetUserProfile(int userId);
    }
}
