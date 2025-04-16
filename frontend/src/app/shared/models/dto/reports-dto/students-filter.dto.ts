export interface StudentsFilter{
    teacherId: number | null;
    studentId: number | null;
    page: number;
    perPage: number;
}