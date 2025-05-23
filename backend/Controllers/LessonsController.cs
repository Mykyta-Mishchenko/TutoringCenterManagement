﻿using backend.DTO.LessonsDTO;
using backend.Extensions;
using backend.Interfaces.Services;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.EventSource;
using SixLabors.ImageSharp.Formats.Bmp;
using System.Security.Claims;

namespace backend.Controllers
{
    [Route("api/lessons")]
    [ApiController]
    [Authorize]
    public class LessonsController : ControllerBase
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
            var lessons = await _lessonsService.GetUserLessonsAsync(userId);

            if(lessons == null)
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

            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            if(userId == null || userId.Value != lesson.UserId.ToString())
            {
                return Unauthorized("User doesn't exists!");
            }

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

        [HttpPut("update")]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> UpdateLesson(LessonEditDTO lesson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetAllErrors());
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userId == null || userId.Value != lesson.UserId.ToString())
            {
                return Unauthorized("User doesn't exists!");
            }

            var result = await _lessonsService.UpdateTeacherLessonAsync(int.Parse(userId.Value), lesson);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Something went wrong.");
            }
        }

        [HttpDelete("delete/{lessonId:int}")]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> DeleteTeacherLesson(int lessonId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            if(userId == null)
            {
                return Unauthorized();
            }

            var result = await _lessonsService.DeleteTeacherLessonAsync(int.Parse(userId.Value), lessonId);

            if(result.Succeeded)
            {
                return Ok();
            }

            return NotFound("No such lesson.");
        }

        [HttpPost("subscribe/{lessonId:int}")]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> SubscribeTeacherLesson(int lessonId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var result = await _lessonsService.CreateStudentLessonAsync(int.Parse(userId.Value), lessonId);

            if (result.Failed)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("unsubscribe/{lessonId:int}")]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> UnsubscribeTeacherLesson(int lessonId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var result = await _lessonsService.DeleteStudentLessonAsync(int.Parse(userId.Value), lessonId);

            if (result.Failed)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
