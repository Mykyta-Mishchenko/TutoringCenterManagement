using backend.Attributes;
using backend.DTO.LessonsDTO;
using backend.Extensions;
using backend.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.ExternalApiControllers
{
    [Route("external-api")]
    [ApiController]
    public class ExternalApi : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IReportsService _reportsService;
        private readonly ILessonsService _lessonsService;
        private readonly IAnalyticsService _analyticsService;
        public ExternalApi(
            IWebHostEnvironment environment,
            IReportsService reportsService,
            ILessonsService lessonsService,
            IAnalyticsService analyticsService
            )
        {
            _reportsService = reportsService;
            _lessonsService = lessonsService;
            _analyticsService = analyticsService;
            _environment = environment;
        }
        [HttpGet("download/teacher/reports/{teacherId:int}")]
        [Authorize(Roles = "ApiClient, teacher")]
        public async Task<IActionResult> DownloadTeacherReports(int teacherId)
        {
            var reports = await _reportsService.GetTeacherReportsAsync(teacherId);

            return Ok(reports);
        }

        [HttpGet("download/teacher/schedule/{teacherId:int}")]
        [Authorize(Roles = "ApiClient, teacher")]
        public async Task<IActionResult> DownloadTeacherSchedule(int teacherId)
        {
            var lessons = await _lessonsService.GetUserLessonsAsync(teacherId);

            return Ok(lessons);
        }

        [HttpGet("download/teacher/salary-reports/{teacherId:int}")]
        [Authorize(Roles = "ApiClient, teacher")]
        public async Task<IActionResult> DownloadTeacherSalaryReports(int teacherId)
        {
            var reports = await _analyticsService.GetTeacherSalaryReportsAsync(teacherId);

            return Ok(reports);
        }

        [HttpPost("create/lesson")]
        //[JsonSchemaValidation("Schemas/LessonSchema.json")]
        [Authorize(Roles = "ApiClient")]
        public async Task<IActionResult> CreateTeacherLesson(LessonCreateDTO lesson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetAllErrors());
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("User doesn't exists!");
            }

            lesson.UserId = int.Parse(userId.Value);

            var result = await _lessonsService.CreateTeacherLessonAsync(int.Parse(userId.Value), lesson);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Something went wrong.");
            }
        }

        [HttpGet("schemas/{schemaName}")]
        public async Task<IActionResult> GetSchema(string schemaName)
        {
            if (string.IsNullOrWhiteSpace(schemaName) || schemaName.Contains("..") || schemaName.Contains("/") || schemaName.Contains("\\"))
            {
                return BadRequest("Invalid schema name");
            }

            if (!schemaName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            {
                schemaName += ".json";
            }

            var schemaPath = Path.Combine(_environment.ContentRootPath, "Schemas", schemaName);

            if (!System.IO.File.Exists(schemaPath))
            {
                return NotFound($"Schema '{schemaName}' not found");
            }

            var schemaContent = await System.IO.File.ReadAllTextAsync(schemaPath);
            return Content(schemaContent, "application/schema+json");
        }
    }
}
