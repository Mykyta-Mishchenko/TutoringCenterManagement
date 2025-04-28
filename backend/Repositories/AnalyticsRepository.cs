using backend.Data.DataModels;
using backend.DTO.AnalyticsDTO;
using backend.Interfaces.Repositories;
using JwtBackend.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace backend.Repositories
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private readonly IdentityDbContext _dbContext;
        public AnalyticsRepository(IdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StudentAnalyticsDTO> GetStudentMarksAnalyticsAsync(AnalyticsFilterDTO filter)
        {
            var query = _dbContext.Marks
                .Where(r => r.Report.StudentLesson.StudentId == filter.StudentId)
                .Where(r => r.Report.DateTime.Month > DateTime.Now.Month - filter.Months);

            if (filter.IsSearching)
            {
                query = query.Where(r => r.Report.StudentLesson.TeacherLesson.TeacherId == filter.TeacherId);
            }

            return await GetMarksFromQuery(query, filter);
        }

        public async Task<SalaryAnalyticsDTO> GetStudentPriceAnalyticsAsync(AnalyticsFilterDTO filter)
        {
            var query = _dbContext.Reports
                .Where(r => r.StudentLesson.StudentId == filter.StudentId)
                .Where(r => r.DateTime.Month > DateTime.Now.Month - filter.Months);

            if (filter.IsSearching)
            {
                query = query.Where(r => r.StudentLesson.TeacherLesson.TeacherId == filter.TeacherId);
            }

            return await GetPriceFromQuery(query, filter);
        }

        public async Task<ICollection<SalaryReportDTO>> GetStudentPriceReportsAsync(SalaryFilterDTO filter)
        {
            var query = _dbContext.Reports
                .Where(r => r.StudentLesson.StudentId == filter.StudentId)
                .Where(r => r.DateTime.Month == DateTime.Now.Month);
            if (filter.IsSearching)
            {
                query = query.Where(r => r.StudentLesson.TeacherLesson.TeacherId == filter.TeacherId);
            }


            return await query.GroupBy(r => new 
                { 
                    r.StudentLesson.TeacherLesson.TeacherId, 
                    r.StudentLesson.TeacherLesson.User.FirstName,
                    r.StudentLesson.TeacherLesson.User.LastName 
                })
                .Select(g => new SalaryReportDTO
                {
                    Id = g.Key.TeacherId,
                    StudentFullName = "",
                    TeacherFullName = $"{g.Key.FirstName} {g.Key.LastName}",
                    LessonsCount = g.Count(),
                    Price = g.Sum(r => r.StudentLesson.TeacherLesson.LessonType.Price)
                }).ToListAsync();
        }

        public async Task<StudentAnalyticsDTO> GetTeacherMarksAnalyticsAsync(AnalyticsFilterDTO filter)
        {
            var query = _dbContext.Marks
                .Where(r => r.Report.StudentLesson.TeacherLesson.TeacherId == filter.TeacherId)
                .Where(r => r.Report.DateTime.Month > DateTime.Now.Month - filter.Months);

            if (filter.IsSearching)
            {
                query = query.Where(r => r.Report.StudentLesson.StudentId == filter.StudentId);
            }

            return await GetMarksFromQuery(query, filter);
        }

        public async Task<SalaryAnalyticsDTO> GetTeacherSalaryAnalyticsAsync(AnalyticsFilterDTO filter)
        {
            var query = _dbContext.Reports
                .Where(r => r.StudentLesson.TeacherLesson.TeacherId == filter.TeacherId)
                .Where(r => r.DateTime.Month > DateTime.Now.Month - filter.Months);

            if (filter.IsSearching)
            {
                query = query.Where(r => r.StudentLesson.StudentId == filter.StudentId);
            }

            return await GetPriceFromQuery(query, filter);
        }

        public async Task<ICollection<SalaryReportDTO>> GetTeacherSalaryReportsAsync(int teacherId)
        {
            return await _dbContext.Reports
                .Where(r => r.StudentLesson.TeacherLesson.TeacherId == teacherId)
                .GroupBy(r => new
                {
                    r.StudentLesson.StudentId,
                    r.StudentLesson.User.FirstName,
                    r.StudentLesson.User.LastName
                })
                .Select(g => new SalaryReportDTO
                {
                    Id = g.Key.StudentId,
                    StudentFullName = $"{g.Key.FirstName} {g.Key.LastName}",
                    TeacherFullName = "",
                    LessonsCount = g.Count(),
                    Price = g.Sum(r => r.StudentLesson.TeacherLesson.LessonType.Price)
                }).ToListAsync();
        }

        public async Task<ICollection<SalaryReportDTO>> GetTeacherSalaryReportsAsync(SalaryFilterDTO filter)
        {
            var query = _dbContext.Reports
                .Where(r => r.StudentLesson.TeacherLesson.TeacherId == filter.TeacherId)
                .Where(r => r.DateTime.Month == DateTime.Now.Month);
            if (filter.IsSearching)
            {
                query = query.Where(r => r.StudentLesson.StudentId == filter.StudentId);
            }


            return await query.GroupBy(r => new 
                { 
                    r.StudentLesson.StudentId, 
                    r.StudentLesson.User.FirstName, 
                    r.StudentLesson.User.LastName 
                })
                .Select(g => new SalaryReportDTO
                {
                    Id = g.Key.StudentId,
                    StudentFullName = $"{g.Key.FirstName} {g.Key.LastName}",
                    TeacherFullName = "",
                    LessonsCount = g.Count(),
                    Price = g.Sum(r => r.StudentLesson.TeacherLesson.LessonType.Price)
                }).ToListAsync();
        }

        private async Task<SalaryAnalyticsDTO> GetPriceFromQuery(IQueryable<Report> query, AnalyticsFilterDTO filter)
        {
            if (filter.Months == 1)
            {
                var reports = await query
                    .OrderBy(r => r.DateTime)
                    .Select(r => new {
                        price = r.StudentLesson.TeacherLesson.LessonType.Price,
                        date = $"Day {r.DateTime.Day}"
                    })
                    .ToListAsync();

                return new SalaryAnalyticsDTO
                {
                    Data = reports.Select(r => r.price).ToArray(),
                    TimeLabels = reports.Select(r => r.date).ToArray()
                };
            }
            else
            {
                var reports = await query
                    .GroupBy(r => r.DateTime.Date.Month)
                    .OrderBy(g=>g.Key)
                    .Select(g => new {
                        price = g.Sum(r => r.StudentLesson.TeacherLesson.LessonType.Price),
                        date = $"Month {CultureInfo.InvariantCulture.DateTimeFormat.GetAbbreviatedMonthName(g.Key)}"
                    })
                    .ToListAsync();

                return new SalaryAnalyticsDTO
                {
                    Data = reports.Select(r => r.price).ToArray(),
                    TimeLabels = reports.Select(r => r.date).ToArray()
                };
            }
        }
        private async Task<StudentAnalyticsDTO> GetMarksFromQuery(IQueryable<Mark> query, AnalyticsFilterDTO filter)
        {
            var marks = await query
                .OrderBy(m => m.Report.DateTime)
                .GroupBy(m => new { m.Report.DateTime, m.MarkType.Name })
                .Select(g => new
                {
                    name = g.Key.Name,
                    score = g.Average(m => m.Score),
                    date = g.Key.DateTime,
                })
                .ToListAsync();

            var markTypes = await _dbContext.MarkTypes.Select(t => t.Name).ToListAsync();

            var result = new StudentAnalyticsDTO
            {
                Marks = [],
                TimeLabels = marks.Select(m => $"Day {m.date.Day}").Distinct().ToArray()
            };

            foreach (var markType in markTypes)
            {
                result.Marks.Add(new MarkAnalyticsDTO
                {
                    Data = marks.Where(m=>m.name == markType).Select(m => m.score).ToArray(),
                    MarkLabel = markType
                });
            }
            return result;
        }
    }
}
