using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace backend.Data.DataModels
{
    public class Report
    {
        [BindNever]
        public int ReportId { get; set; }
        public int LessonId { get; set; }
        public string Description { get; set; }
        public DateTime DateTime {  get; set; }

        public StudentLesson StudentLesson { get; set; }
        public ICollection<Mark> Marks { get; set; }
    }
}
