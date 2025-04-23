using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace backend.Data.DataModels
{
    public class StudentLesson
    {
        [BindNever]
        public int StudentLessonId { get; set; }
        public int LessonId { get; set; }
        public int StudentId { get; set;}

        public User User { get; set; }
        public TeacherLesson TeacherLesson { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
}
