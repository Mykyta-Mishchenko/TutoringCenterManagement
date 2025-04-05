using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/teachers")]
    [ApiController]
    [Authorize]
    public class TeachersController : Controller
    {

        /*[HttpGet("info")]
        public async Task<IActionResult> GetTeachers()
        {

        }*/
    }
}
