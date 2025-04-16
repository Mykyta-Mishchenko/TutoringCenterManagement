import { MarkDTO } from "./mark.dto";

export interface ReportCreatingDTO{
    teacherId: number;
    studentId: number;
    date: Date;
    marks: MarkDTO[];
    description: string;
}