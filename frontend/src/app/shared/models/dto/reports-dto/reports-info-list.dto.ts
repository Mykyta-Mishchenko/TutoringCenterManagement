import { ReportDTO } from "./report.dto";

export interface ReportsInfoList{
    totalPageNumber: number;
    reportsList: ReportDTO[];
}