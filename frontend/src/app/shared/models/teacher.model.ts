import { Subject } from "./subject.model";

export interface Teacher{
    id: number;
    name: string;
    subjects: Subject[];
    profileImgUrl: string;
}