using AutoMapper;
using backend.Data.DataModels;
using backend.DTO;
using backend.DTO.AuthDTO;
using backend.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace JwtBackend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        
        public AuthorizationController(
            IMapper mapper,
            IAuthService authService,
            ITokenService tokenService
            ) 
        {
            _mapper = mapper;
            _authService = authService;
            _tokenService = tokenService;
        }
        [HttpPost("signIn")]
        public async Task<IActionResult> SignIn(SignInDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Check your sign in form.");
            }
            var user = _mapper.Map<User>(request);
            var result = await _authService.SignIn(user);

            if (!result.Succeeded)
            {
                return Unauthorized("Invalid email or password.");
            }

            _tokenService.AppendRefreshToken(HttpContext,result.Tokens.RefreshToken);

            return Ok(new { result.Tokens.AccessToken });
           
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp(SignUpDTO request)
        {
            if (!ModelState.IsValid || request.Password != request.ConfirmedPassword)
            {
                return BadRequest("Check your sign up form.");
            }

            var user = _mapper.Map<User>(request);
            var result = await _authService.SignUp(user, request.Role);
            if (!result.Succeeded)
            {
                return BadRequest("Can't sign up user. Try again.");
            }
            return Ok();
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if(refreshToken == null)
            {
                return Unauthorized();
            }

            var result = await _authService.RefreshSession(refreshToken);
            if(result != null)
            {
                _tokenService.AppendRefreshToken(HttpContext, result.RefreshToken);
                return Ok(new { result.AccessToken });
            }

            return Unauthorized();
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            _authService.Logout(refreshToken ?? "");
            return Ok();
        }
    }
}
