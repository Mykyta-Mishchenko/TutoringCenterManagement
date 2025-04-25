using backend.DTO.AnalyticsDTO;
using backend.Interfaces.Services;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;
        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }
        [HttpGet("teacher/salary")]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> GetTeacherSalaryReports([FromQuery] SalaryFilterDTO filter)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            if(userId == null || int.Parse(userId.Value) != filter.TeacherId)
            {
                return Unauthorized();
            }

            var reports = await _analyticsService.GetSalaryReportsAsync(filter, UserRole.teacher);
            return Ok(reports);
        }

        [HttpGet("student/price")]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> GetStudentSalaryReports([FromQuery] SalaryFilterDTO filter)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userId == null || int.Parse(userId.Value) != filter.StudentId)
            {
                return Unauthorized();
            }

            var reports = await _analyticsService.GetSalaryReportsAsync(filter, UserRole.student);
            return Ok(reports);
        }

        [HttpGet("teacher/salary/analytics")]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> GetTeacherSalaryAnalytics([FromQuery] AnalyticsFilterDTO filter)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userId == null || int.Parse(userId.Value) != filter.TeacherId)
            {
                return Unauthorized();
            }

            var reports = await _analyticsService.GetSalaryAnalyticsAsync(filter, UserRole.teacher);
            return Ok(reports);
        }

        [HttpGet("student/price/analytics")]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> GetStudentPriceAnalytics([FromQuery] AnalyticsFilterDTO filter)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userId == null || int.Parse(userId.Value) != filter.StudentId)
            {
                return Unauthorized();
            }

            var reports = await _analyticsService.GetSalaryAnalyticsAsync(filter, UserRole.student);
            return Ok(reports);
        }

        [HttpGet("teacher/marks/analytics")]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> GetTeacherMarksAnalytics([FromQuery] AnalyticsFilterDTO filter)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userId == null || int.Parse(userId.Value) != filter.TeacherId)
            {
                return Unauthorized();
            }

            var reports = await _analyticsService.GetMarksAnalyticsAsync(filter, UserRole.teacher);
            return Ok(reports);
        }

        [HttpGet("student/marks/analytics")]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> GetStudentMarksAnalytics([FromQuery] AnalyticsFilterDTO filter)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userId == null || int.Parse(userId.Value) != filter.StudentId)
            {
                return Unauthorized();
            }

            var reports = await _analyticsService.GetMarksAnalyticsAsync(filter, UserRole.student);
            return Ok(reports);
        }
    }
}
