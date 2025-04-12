import { Roles } from "../roles.enum";

export interface UsersFilter{
    role: Roles | null;
    name: string | null;
    subjectId: number;
    schoolYear: number;
    page: number;
    perPage: number;
}