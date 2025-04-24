using backend.DTO.ReportsDTO;

namespace backend.Comparers
{
    public class ReportScheduleComparer : IEqualityComparer<ReportScheduleDTO>
    {
        public bool Equals(ReportScheduleDTO x, ReportScheduleDTO y)
        {
            return x.LessonId == y.LessonId && x.Date == y.Date;
        }

        public int GetHashCode(ReportScheduleDTO obj)
        {
            return HashCode.Combine(obj.LessonId, obj.Date);
        }
    }
}
