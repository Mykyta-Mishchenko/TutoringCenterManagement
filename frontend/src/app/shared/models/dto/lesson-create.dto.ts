export interface LessonCreateDTO{
    userId: number;
    subjectId: number;
    schoolYear: number;
    maxStudentsCount: number;
    day: number;
    hour: number;
    minutes: number;
    price: number;
}