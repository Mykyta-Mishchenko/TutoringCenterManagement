using backend.Interfaces.Repositories;
using backend.Interfaces.Services;
using System.Text;
using System.Security.Cryptography;
using Host = Microsoft.AspNetCore.Hosting;

namespace backend.Services
{
    public class ProfileService : IProfileService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserRepository _userRepository;
        private readonly Host.IHostingEnvironment _environment;
        public ProfileService(
            ISessionRepository sessionRepository,
            IUserRepository userRepository,
            Host.IHostingEnvironment environment) 
        {
            _sessionRepository = sessionRepository;
            _userRepository = userRepository;
            _environment = environment;
        }
        public async Task<byte[]> GetUserProfileAsync(int userId)
        {
            var filePath =  await _userRepository.GetUserProfileAsync(userId);
            if (string.IsNullOrEmpty(filePath))
            {
                return null;
            }
            var imageBytes = File.ReadAllBytes(filePath);

            return imageBytes;
        }

        public async Task<string> SetUserProfileAsync(string refreshToken, IFormFile file)
        {
            var session = await _sessionRepository.GetSessionAsync(refreshToken);

            if (session != null)
            {
                var imgPath =  await _userRepository.GetUserProfileAsync(session.UserId);

                if(imgPath != null)
                {
                    DeletePreviousImg(imgPath);
                }

                return await SaveProfileImgAsync(session.UserId, file);
            }
            return string.Empty;
        }
        private void DeletePreviousImg(string imgPath)
        {
            if (File.Exists(imgPath))
            {
                File.Delete(imgPath);
            }
        }

        private async Task<string> SaveProfileImgAsync(int userId, IFormFile file)
        {
            var fileName = file.FileName + DateTime.Now.ToString();
            using (var sha = new SHA256Managed())
            {
                byte[] textData = Encoding.UTF8.GetBytes(fileName);
                byte[] hash = sha.ComputeHash(textData);
                fileName = BitConverter.ToString(hash).Replace("-", "");
            }
            var filePath = Path.Combine(_environment.ContentRootPath, "ProfileImages", fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            await _userRepository.SetUserProfileAsync(userId, filePath);
            return filePath;
        }
    }
}
