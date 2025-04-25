import { MarkAnalyticsDTO } from "./mark-analytics.dto";

export interface StudentAnalyticsDTO{
    marks: MarkAnalyticsDTO[]
    timeLabels: string[];
}