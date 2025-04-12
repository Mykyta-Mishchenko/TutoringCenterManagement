import { LessonType } from "./lesson-type.model";
import { Schedule } from "./schedule.model";

export interface Lesson{
    lessonId: number;
    teacherId: number;
    lessonType: LessonType;
    schedule: Schedule;
    studentsCount: number;
}