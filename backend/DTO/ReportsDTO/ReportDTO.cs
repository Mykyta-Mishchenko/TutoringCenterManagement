using Microsoft.Identity.Client;

namespace backend.DTO.ReportsDTO
{
    public class ReportDTO
    {
        public int ReportId { get; set; }
        public int StudentId { get; set; }
        public string TeacherFullName { get; set; }
        public string StudentFullName { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public ICollection<MarkDTO> Marks { get; set; }
    }
}
