import { LessonGroup } from "./lesson-group.model";
import { Subject } from "./subject.model";

export interface LessonType {
    typeId: number;
    group: LessonGroup;
    subject: Subject;
    price: number;
}