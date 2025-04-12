import { SubjectInfo } from "./subject-info.dto";

export interface UserInfo{
    userId: number;
    fullName: string;
    subjects: SubjectInfo[];
}