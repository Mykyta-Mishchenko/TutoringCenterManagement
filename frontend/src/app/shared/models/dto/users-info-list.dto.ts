import { UserInfo } from "./user-info.dto";

export interface UsersInfoList{
    totalPageNumber: number;
    usersList: UserInfo[];
}