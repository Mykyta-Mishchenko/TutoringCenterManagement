using AutoMapper;
using backend.Comparers;
using backend.Data.DataModels;
using backend.DTO.ReportsDTO;
using backend.Interfaces.Repositories;
using backend.Interfaces.Services;
using backend.Models;
using Microsoft.Identity.Client;

namespace backend.Services
{
    public class ReportsService : IReportsService
    {
        private readonly IMapper _mapper;
        private readonly IMarkRepository _markRepository;
        private readonly IReportsRepository _reportsRepository;
        private readonly ILessonsRepository _lessonsRepository;
        private readonly IMarkTypesRepository _markTypesRepository;
        public ReportsService(
            IMapper mapper,
            IMarkRepository markRepository,
            IReportsRepository reportsRepository,
            ILessonsRepository lessonsRepository,
            IMarkTypesRepository markTypesRepository)
        {
            _mapper = mapper;
            _markRepository = markRepository;
            _reportsRepository = reportsRepository;
            _lessonsRepository = lessonsRepository;
            _markTypesRepository = markTypesRepository;
        }

        public async Task<OperationResult> CreateReport(ReportCreatingDTO createdReport)
        {
            var lesson = await _lessonsRepository.GetStudentLessonAsync(createdReport.StudentId, createdReport.TeacherLessonId);

            if(lesson == null)
            {
                return OperationResult.Failure;
            }

            var report = new Report
            {
                StudentLessonId = lesson.StudentLessonId,
                Description = createdReport.Description,
                DateTime = createdReport.Date
            };

            report = await _reportsRepository.CreateReportAsync(report);

            if(report == null)
            {
                return OperationResult.Failure;
            }

            foreach (var mark in createdReport.Marks)
            {
                var dbMark = new Mark
                {
                    ReportId = report.ReportId,
                    MarkTypeId = mark.MarkTypeId,
                    Score = mark.MarkValue
                };

                await _markRepository.CreateMarkAsync(dbMark);
            }

            return OperationResult.Success;
        }
        public async Task<ReportsListDTO> GetReportsByFilterAsync(ReportsFilterDTO filter, UserRole role)
        {
            if (role == UserRole.student)
            {
                return await _reportsRepository.GetStudentReportsByFilterAsync(filter);
            }
            else if (role == UserRole.teacher)
            {
                var reports = await _reportsRepository.GetTeacherReportsByFilterAsync(filter);
                return reports;
            }
            else
            {
                return new ReportsListDTO();
            }
        }

        public async Task<ICollection<MarkTypeDTO>> GetMarkTypesAsync()
        {
            var markTypes = await _markTypesRepository.GetAllMarkTypesAsync();
            return _mapper.Map<ICollection<MarkTypeDTO>>(markTypes);
        }

        public async Task<ICollection<ReportScheduleDTO>> GetUnassignedReportsAsync(int teacherId, int studentId)
        {
            var lessons = await _lessonsRepository.GetTeacherLessonsByStudentAsync(teacherId, studentId);

            var allPossibleReports = GetAllPossibelMonthReports(lessons);

            var reports = await _reportsRepository.GetStudentMonthReportsAsync(teacherId, studentId);

            return allPossibleReports.Except(reports, new ReportScheduleComparer()).OrderBy(r => r.Date).ToList();
        }

        public async Task<ReportDTO?> GetReportByIdAsync(int reportId)
        {
            return await _reportsRepository.GetReportDTOByIdAsync(reportId);
        }

        public async Task<OperationResult> UpdateReportAsync(ReportEditingDTO report)
        {
            var dbReport = await _reportsRepository.GetReportByIdAsync(report.ReportId);

            if(dbReport == null)
            {
                return OperationResult.Failure;
            }

            dbReport.Description = report.Description;

            await _reportsRepository.UpdateReportAsync(dbReport);

            if(dbReport == null)
            {
                return OperationResult.Failure;
            }

            return OperationResult.Success;
        }

        private IList<ReportScheduleDTO> GetAllPossibelMonthReports(IList<TeacherLesson> lessons)
        {
            var now = DateTime.Now;
            var firstDayOfMonth = new DateTime(now.Year, now.Month, 1);

            var matchingDates = new List<ReportScheduleDTO>();

            foreach (var lesson in lessons)
            {
                for (int i = 0; i < now.Day; i++)
                {
                    var currentDate = firstDayOfMonth.AddDays(i);

                    if ((int)currentDate.DayOfWeek == lesson.Schedule.DayOfWeek)
                    {
                        var fullDateTime = currentDate.Date + lesson.Schedule.DayTime;
                        matchingDates.Add(new ReportScheduleDTO { LessonId = lesson.LessonId, Date = fullDateTime });
                    }
                }
            }
            return matchingDates;
        }
    }
}
