using backend.DTO.LessonsDTO;
using backend.Extensions;
using backend.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.ExternalApiControllers
{
    [Route("external-api/lessons")]
    [ApiController]
    public class LessonsExternalApi : ControllerBase
    {
        private readonly ILessonsService _lessonsService;
        public LessonsExternalApi(
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
        public async Task<IActionResult> GetUserLessons([FromQuery] int userId)
        {
            var lessons = await _lessonsService.GetUserLessonsAsync(userId);

            if (lessons == null)
            {
                return NotFound("This user doesn't have any lessons.");
            }

            return Ok(lessons);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateLesson(LessonCreateDTO lesson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetAllErrors());
            }

            var result = await _lessonsService.CreateTeacherLessonAsync(lesson.UserId, lesson);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Something went wrong.");
            }
        }

        [HttpPost("subscribe/{lessonId:int}")]
        public async Task<IActionResult> SubscribeTeacherLesson(int lessonId, [FromQuery] int studentId)
        {
            var result = await _lessonsService.CreateStudentLessonAsync(studentId, lessonId);

            if (result.Failed)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
