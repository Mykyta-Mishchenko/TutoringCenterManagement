import { Subject } from "./subject.model";

export interface Teacher{
    id: number;
    fullName: string;
    subjects: Subject[];
    profileImgUrl: string | null;
}