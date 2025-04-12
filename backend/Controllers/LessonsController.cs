using backend.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.EventSource;

namespace backend.Controllers
{
    [Route("api/lessons")]
    [ApiController]
    //[Authorize]
    public class LessonsController : Controller
    {
        private readonly ILessonsService _lessonsService;
        public LessonsController(
            ILessonsService lessonsService)
        {
            _lessonsService = lessonsService;
        }

        [HttpGet("subjects")]
        public async Task<IActionResult> GetAllSubjects()
        {
            var subjects = await _lessonsService.GetAllSubjectsAsync();
            return Ok(subjects);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserSubjects([FromQuery]int userId)
        {
            var lessons = await _lessonsService.GetUserLessons(userId);

            if(lessons == null)
            {
                return NotFound("This user doesn't have any lessons.");
            }

            return Ok(lessons);
        }
    }
}
