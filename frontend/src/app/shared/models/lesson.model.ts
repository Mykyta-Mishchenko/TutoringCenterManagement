import { LessonType } from "./lesson-type.model";
import { Schedule } from "./schedule.model";

export interface Lesson{
    lessonId: number;
    teacherId: number;
    type: LessonType;
    schedule: Schedule;
    studentsCount: number;
}