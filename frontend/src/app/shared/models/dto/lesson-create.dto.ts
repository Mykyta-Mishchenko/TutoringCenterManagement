export interface LessonDTO{
    lessonId: number | null;
    subjectId: number;
    schoolYear: number;
    maxStudentsCount: number;
    day: number;
    hour: number;
    minutes: number;
    price: number;
}