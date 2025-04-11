namespace backend.Data.DataModels
{
    public class StudentLesson
    {
        public int LessonId { get; set; }
        public int StudentId { get; set;}

        public User User { get; set; }
        public TeacherLesson TeacherLesson { get; set; }
    }
}
