using backend.DTO.ReportsDTO;
using backend.Interfaces.Services;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.ExternalApiControllers
{
    [Route("external-api/reports")]
    [ApiController]
    public class ReportsExternalApi : ControllerBase
    {
        private readonly IReportsService _reportsService;
        public ReportsExternalApi(IReportsService reportsService)
        {
            _reportsService = reportsService;
        }
        [HttpGet("empty")]
        public async Task<IActionResult> GetEmptyReportsSchedule([FromQuery] int teacherId, [FromQuery] int studentId)
        {
            var schedule = await _reportsService.GetUnassignedReportsAsync(teacherId, studentId);

            return Ok(schedule);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNewReport(ReportCreatingDTO report)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _reportsService.CreateReport(report);

            if (result == OperationResult.Failure)
            {
                return BadRequest("Something went wrong");
            }

            return Ok();
        }
    }
}
