import { Lesson } from "./lesson.model";

export interface Teacher{
    id: number;
    fullName: string;
    lessons: Lesson[];
    profileImgUrl: string | null;
}