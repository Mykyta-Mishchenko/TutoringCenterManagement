using backend.Data.DataModels;

namespace backend.DTO.ReportsDTO
{
    public class ReportCreatingDTO
    {
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public int TeacherLessonId { get; set; }
        public DateTime Date {  get; set; }
        public string Description { get; set; }
        public IList<MarkDTO> Marks { get; set; }
    }
}
