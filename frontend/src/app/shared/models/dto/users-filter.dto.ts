import { Roles } from "../roles.enum";

export interface UsersFilter{
    role: Roles | null;
    name: string | null;
    subjectId: number | null;
    schoolYear: number | null;
    page: number;
    perPage: number;
}