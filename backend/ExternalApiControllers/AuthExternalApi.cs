using AutoMapper;
using backend.Data.DataModels;
using backend.DTO.AuthDTO;
using backend.DTO.ExternalApiDTO;
using backend.Interfaces.Services;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.ExternalApiControllers
{
    [Route("external-api/auth")]
    [ApiController]
    public class AuthExternalApi : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public AuthExternalApi(
            IMapper mapper,
            IAuthService authService,
            ITokenService tokenService
            )
        {
            _mapper = mapper;
            _authService = authService;
        }

        [HttpPost("signIn")]
        public async Task<IActionResult> SignIn(SignInDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Check your sign in fields.");
            }
            var user = _mapper.Map<User>(request);
            var result = await _authService.SignInAsync(user);

            if (!result.Succeeded)
            {
                return Unauthorized("Invalid email or password.");
            }


            return Ok(result.Tokens);

        }

        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp(ApiSignUpDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Check your sign up fields.");
            }

            var user = _mapper.Map<User>(request);
            var result = await _authService.SignUpAsync(user, UserRole.apiClient);
            if (!result.Succeeded)
            {
                return BadRequest("Can't sign up client. Try again.");
            }
            return Ok();
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            if (refreshToken == null)
            {
                return Unauthorized();
            }

            var result = await _authService.RefreshSessionAsync(refreshToken);
            if (result != null)
            {
                return Ok(result);
            }

            return Unauthorized();
        }
    }
}
