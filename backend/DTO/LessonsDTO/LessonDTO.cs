using backend.Data.DataModels;

namespace backend.DTO.LessonsDTO
{
    public class LessonDTO
    {
        public int LessonId { get; set; }
        public int TeacherId { get; set; }
        public LessonTypeDTO LessonType { get; set; }
        public ScheduleDTO Schedule { get; set; }
        public int StudentsCount {  get; set; } 
    }
}
