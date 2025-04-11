using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace backend.Data.DataModels
{
    public class LessonType
    {
        [BindNever]
        public int TypeId { get; set; }
        public int SubjectId { get; set; }
        public int SchoolYear { get; set; }
        public int MaxStudentsCount { get; set; }
        public int Price { get; set; }


        public Subject Subject { get; set; }
        public ICollection<TeacherLesson> Lessons { get; set; }
    }
}
