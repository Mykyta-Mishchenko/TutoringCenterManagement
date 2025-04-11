import { Subject } from "./subject.model";

export interface LessonType {
    typeId: number;
    subject: Subject;
    maxStudentsCount: number;
    schoolYear: number;
    price: number;
}