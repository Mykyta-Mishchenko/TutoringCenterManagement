import { MarkDTO } from "./mark.dto";

export interface ReportDTO{
    reportId: number;
    studentId: number;
    teacherFullName: string;
    studentFullName: string;
    description: string;
    date: Date;
    marks: MarkDTO[];
}