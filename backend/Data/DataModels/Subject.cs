using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace backend.Data.DataModels
{
    public class Subject
    {
        [BindNever]
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }

        public ICollection<LessonType> LessonTypes { get; set; }
    }
}
