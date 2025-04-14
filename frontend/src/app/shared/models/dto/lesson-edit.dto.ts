export interface LessonEditDTO{
    lessonId: number;
    userId: number;
    subjectId: number;
    schoolYear: number;
    maxStudentsCount: number;
    day: number;
    hour: number;
    minutes: number;
    price: number;
}