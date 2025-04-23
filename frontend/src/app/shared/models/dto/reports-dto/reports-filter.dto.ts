export interface ReportsFilter{
    isSearching: boolean;
    teacherId: number | null;
    studentId: number | null;
    page: number;
    perPage: number;
}