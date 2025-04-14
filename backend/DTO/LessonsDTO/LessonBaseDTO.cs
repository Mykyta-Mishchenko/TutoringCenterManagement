namespace backend.DTO.LessonsDTO
{
    public class LessonBaseDTO
    {
        public int UserId { get; set; }
        public int SubjectId { get; set; }
        public int SchoolYear { get; set; }
        public int MaxStudentsCount { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minutes { get; set; }
        public int Price { get; set; }
    }
}
