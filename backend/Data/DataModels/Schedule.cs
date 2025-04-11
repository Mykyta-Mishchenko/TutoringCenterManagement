using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace backend.Data.DataModels
{
    public class Schedule
    {
        [BindNever]
        public int DateId { get; set; }
        public int DayOfWeek { get; set; }
        public DateTime DayTime { get; set; }

        public ICollection<TeacherLesson> Lessons { get; set; }
    }
}
