using backend.DTO.ExternalApiDTO;
using backend.DTO.UsersDTO;
using backend.Interfaces.Services;
using JwtBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.ExternalApiControllers
{
    [Route("external-api/profile")]
    [ApiController]
    //[Authorize(Roles = "ApiClient")]
    public class ProfileExternalApi : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly IUsersService _usersService;

        public ProfileExternalApi(
            IProfileService profileService,
            IUsersService usersService)
        {
            _profileService = profileService;
            _usersService = usersService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> SetUserProfileImg(ApiProfileDTO userProfile)
        {
            var refreshToken = userProfile.RefreshToken;

            if (ModelState.IsValid)
            {
                await _profileService.SetUserProfileByRefreshTokenAsync(refreshToken, userProfile.ProfileImg);
            }
            return Ok();
        }

        [HttpPost("upload/{email}")]
        public async Task<IActionResult> SetUserProfileImg(string email, UserProfileDTO userProfile)
        {

            if (ModelState.IsValid)
            {
                await _profileService.SetUserProfileByEmailTokenAsync(email, userProfile.ProfileImg);
            }
            return Ok();
        }

        [HttpGet("image/{email}")]
        public async Task<IActionResult> GetProfileImage(string email)
        {
            var user = await _usersService.GetUserByEmail(email);
            var accessTokenUserId = user.UserId.ToString();//User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (user == null || user.UserId.ToString() != accessTokenUserId)
            {
                return Unauthorized();
            }

            var imageBytes = await _profileService.GetUserProfileAsync(user.UserId);
            if (imageBytes == null)
            {
                return NotFound();
            }
            return File(imageBytes, "image/jpeg");
        }
    }
}
