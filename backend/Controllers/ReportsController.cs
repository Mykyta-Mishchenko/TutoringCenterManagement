using backend.DTO.ReportsDTO;
using backend.Interfaces.Services;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace backend.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly IReportsService _reportsService;
        private readonly ILessonsService _lessonsService;

        public ReportsController(
            IReportsService reportsService,
            ILessonsService lessonsService)
        {
            _reportsService = reportsService;
            _lessonsService = lessonsService;
        }

        [HttpGet("student/reports")]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> GetStudentReportsByFilter([FromQuery] ReportsFilterDTO filter) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reports = await _reportsService.GetReportsByFilterAsync(filter, UserRole.student);

            return Ok(reports);
        }

        [HttpGet("teacher/reports")]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> GetTeacherReportsByFilter([FromQuery] ReportsFilterDTO filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reports = await _reportsService.GetReportsByFilterAsync(filter, UserRole.teacher);

            return Ok(reports);
        }

        [HttpGet("marks/types")]
        public async Task<IActionResult> GetMarkTypes()
        {
            var markTypes = await _reportsService.GetMarkTypesAsync();

            return Ok(markTypes);
        }

        [HttpGet("teacher/students")]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> GetTeacherStudents([FromQuery] int teacherId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            if(userId == null || int.Parse(userId.Value) != teacherId)
            {
                return Unauthorized();
            }

            var students = await  _lessonsService.GetTeacherStudentsAsync(teacherId);

            return Ok(students);
        }

        [HttpGet("student/teachers")]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> GetStudentTeachers([FromQuery] int studentId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userId == null || int.Parse(userId.Value) != studentId)
            {
                return Unauthorized();
            }

            var teachers = await _lessonsService.GetStudentTeachersAsync(studentId);

            return Ok(teachers);
        }

        [HttpGet("teacher/students/reports/empty")]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> GetEmptyReportsSchedule([FromQuery] int teacherId, [FromQuery] int studentId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            
            if (userId == null || int.Parse(userId.Value) != teacherId)
            {
                return Unauthorized();
            }

            var schedule = await _reportsService.GetUnassignedReportsAsync(teacherId, studentId);

            return Ok(schedule);
        }

        [HttpPost("reports/add")]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> AddNewReport(ReportCreatingDTO report)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userId == null || int.Parse(userId.Value) != report.TeacherId)
            {
                return Unauthorized();
            }

            var result = await _reportsService.CreateReport(report);

            if(result == OperationResult.Failure)
            {
                return BadRequest("Something went wrong");
            }

            return Ok();
        }

        [HttpPut("reports/edit")]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> EditReport(ReportEditingDTO report)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _reportsService.UpdateReportAsync(report);

            if(result == OperationResult.Failure)
            {
                return BadRequest("Can't update report.");
            }

            return Ok();
        }

        [HttpGet("reports")]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> GetReportById([FromQuery] int reportId)
        {
            var report = await _reportsService.GetReportByIdAsync(reportId);

            if(report == null)
            {
                return NotFound();
            }

            return Ok(report);
        }
    }
}
