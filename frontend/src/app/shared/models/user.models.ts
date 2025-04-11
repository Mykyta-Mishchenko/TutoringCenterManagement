import { Roles } from "./roles.enum";

export interface User{
    userId: number;
    username: string;
    email: string;
    role: Roles;
    profileImgUrl: string | null;
}