namespace backend.DTO.AnalyticsDTO
{
    public class SalaryReportDTO
    {
        public int Id { get; set; }
        public string StudentFullName { get; set; }
        public string TeacherFullName { get; set; }
        public int LessonsCount { get; set; }
        public int Price { get; set; }
    }
}
