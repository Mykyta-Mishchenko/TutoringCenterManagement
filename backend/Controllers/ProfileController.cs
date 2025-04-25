using backend.DTO.UsersDTO;
using backend.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/profile")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> SetUserProfileImg(UserProfileDTO userProfile)
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (ModelState.IsValid)
            {
                await _profileService.SetUserProfileAsync(refreshToken, userProfile.ProfileImg);
            }
            return Ok();
        }

        [HttpGet("image")]
        public async Task<IActionResult> GetProfileImage([FromQuery]int userId)
        {
            var imageBytes = await _profileService.GetUserProfileAsync(userId);
            if(imageBytes == null)
            {
                return NotFound();
            }
            return File(imageBytes, "image/jpeg");
        }
    }
}
