using backend.DTO.UsersDTO;
using backend.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsersByFilter([FromQuery] UsersFilterDTO filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _usersService.GetUsersByFilterAsync(filter);

            return Ok(users);
        }

        [HttpGet("user/role")]
        public async Task<IActionResult> GetUserRole([FromQuery]int userId)
        {
            var role = await _usersService.GetUserRole(userId);

            if(role == null)
            {
                return NotFound("User doesn't have any roles.");
            }

            return Ok(role);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserById([FromQuery] int userId)
        {
            var user = await _usersService.GetUser(userId);

            if(user == null)
            {
                return NotFound("User with such id doesn't exists.");
            }

            return Ok(user);
        }
        
    }
}
