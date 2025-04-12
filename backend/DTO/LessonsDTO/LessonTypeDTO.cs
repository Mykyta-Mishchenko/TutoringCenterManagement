namespace backend.DTO.LessonsDTO
{
    public class LessonTypeDTO
    {
        public int TypeId { get; set; }
        public SubjectDTO Subject { get; set; }
        public int MaxStudentsCount { get; set; }
        public int SchoolYear { get; set; }
        public int Price { get; set; }
    }
}
