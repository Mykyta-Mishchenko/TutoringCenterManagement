using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace backend.Data.DataModels
{
    public class TeacherLesson
    {
        [BindNever]
        public int LessonId { get; set; }
        public int TeacherId { get; set; }
        public int TypeId { get; set; }
        public int ScheduleId { get; set; }

        public User User { get; set; }
        public Schedule Schedule { get; set; }
        public LessonType LessonType { get; set; }
        public ICollection<StudentLesson> StudentLessons { get; set;}
    }
}
