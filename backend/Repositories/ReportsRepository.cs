using backend.Data.DataModels;
using backend.DTO.ReportsDTO;
using backend.Interfaces.Repositories;
using backend.Models;
using JwtBackend.Data;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;

namespace backend.Repositories
{
    public class ReportsRepository : IReportsRepository
    {
        private readonly IdentityDbContext _dbContext;
        public ReportsRepository(IdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ReportsListDTO> GetStudentReportsByFilterAsync(ReportsFilterDTO filter)
        {
            var baseQuery = _dbContext.Reports
                .Where(r => r.StudentLesson.StudentId == filter.StudentId);

            if (filter.IsSearching)
            {
                baseQuery = baseQuery.Where(r => r.StudentLesson.TeacherLesson.TeacherId == filter.TeacherId);
            }

            var totalCount = await baseQuery.CountAsync();

            var totalPages = (int)Math.Ceiling(totalCount / (double)filter.PerPage);

            var reports = await GetReportsFromQueryAsync(baseQuery, filter);

            return new ReportsListDTO
            {
                TotalPageNumber = totalPages,
                ReportsList = reports
            };
        }

        public async Task<ReportsListDTO> GetTeacherReportsByFilterAsync(ReportsFilterDTO filter)
        {
            var baseQuery = _dbContext.Reports
                .Where(r => r.StudentLesson.TeacherLesson.TeacherId == filter.TeacherId);

            if (filter.IsSearching)
            {
                baseQuery = baseQuery.Where(r => r.StudentLesson.StudentId == filter.StudentId);
            }

            var totalCount = await baseQuery.CountAsync();

            var totalPages = (int)Math.Ceiling(totalCount / (double)filter.PerPage);

            var reports = await GetReportsFromQueryAsync(baseQuery, filter);

            return new ReportsListDTO
            {
                TotalPageNumber = totalPages,
                ReportsList = reports
            };
        }

        private async Task<IList<ReportDTO>> GetReportsFromQueryAsync(IQueryable<Report> query, ReportsFilterDTO filter)
        {
            return await query
                .OrderBy(r => r.DateTime)
                .Skip((filter.Page - 1) * filter.PerPage)
                .Take(filter.PerPage)
                .Select(r => new ReportDTO
                {
                    ReportId = r.ReportId,
                    StudentId = filter.StudentId,
                    StudentFullName = $"{r.StudentLesson.User.FirstName} {r.StudentLesson.User.LastName}",
                    TeacherFullName = $"{r.StudentLesson.TeacherLesson.User.FirstName} {r.StudentLesson.TeacherLesson.User.LastName}",
                    Description = r.Description,
                    Date = r.DateTime,
                    Marks = r.Marks.Select(m => new MarkDTO
                    {
                        MarkTypeId = m.MarkTypeId,
                        MarkValue = m.Score
                    }).ToList()
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IList<ReportScheduleDTO>> GetStudentMonthReportsAsync(int teacherId, int studentId)
        {
            return await _dbContext.Reports
                .Where(r => r.StudentLesson.StudentId == studentId &&
                r.StudentLesson.TeacherLesson.TeacherId == teacherId)
                .Where(r => r.DateTime.Month == DateTime.Now.Month)
                .Select(r=>new ReportScheduleDTO
                {
                    LessonId = r.StudentLesson.LessonId,
                    Date = r.DateTime
                })
                .ToListAsync();
        }

        public async Task<Report?> CreateReportAsync(Report report)
        {
            await _dbContext.Reports.AddAsync(report);
            await _dbContext.SaveChangesAsync();
            return report;
        }

        public async Task<ReportDTO?> GetReportDTOByIdAsync(int reportId)
        {
            return await _dbContext.Reports
                .Where(r => r.ReportId == reportId)
                .Select(r => new ReportDTO
                {
                    ReportId = r.ReportId,
                    StudentId = r.StudentLesson.StudentId,
                    StudentFullName = $"{r.StudentLesson.User.FirstName} {r.StudentLesson.User.LastName}",
                    TeacherFullName = $"{r.StudentLesson.TeacherLesson.User.FirstName} {r.StudentLesson.TeacherLesson.User.LastName}",
                    Date = r.DateTime,
                    Description = r.Description,
                    Marks = r.Marks.Select(m => new MarkDTO
                    {
                        MarkTypeId = m.MarkTypeId,
                        MarkValue = m.Score
                    }).ToList()
                }).FirstOrDefaultAsync();
        }

        public async Task<Report?> GetReportByIdAsync(int reportId)
        {
            return await _dbContext.Reports.FirstOrDefaultAsync(r => r.ReportId == reportId);
        }

        public async Task<Report?> UpdateReportAsync(Report report)
        {
            _dbContext.Reports.Update(report);
            await _dbContext.SaveChangesAsync();
            return report;
        }
    }
}
