import { inject, Injectable, OnInit} from "@angular/core";
import { User } from "../../shared/models/user.models";
import { Roles } from "../../shared/models/roles.enum";
import { UsersFilter } from "../../shared/models/dto/users-filter.dto";
import { UserInfo } from "../../shared/models/dto/user-info.dto";
import { UsersInfoList } from "../../shared/models/dto/users-info-list.dto";
import { environment } from "../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class UsersService {

  private apiUrl = environment.apiUrl;
  private httpClient = inject(HttpClient);

  public BasicTeachersFilter: UsersFilter =
  {
    role: Roles.Teacher,
    name: null,
    subjectId: 0,
    schoolYear: 0,
    page: 1,
    perPage: 20
  };

  getUser(userId: number): Observable<UserInfo> {
    return this.httpClient.get<UserInfo>(`${this.apiUrl}/user?userId=${userId}`, { withCredentials: true });
  }

  getUsersByFilter(filter: UsersFilter): Observable<UsersInfoList> {
    const queryString = this.buildQueryString(filter);
    return this.httpClient.get<UsersInfoList>(`${this.apiUrl}/users?${queryString}`, { withCredentials: true });
  }

  getUserRole(userId: number): Observable<Roles> {
    return this.httpClient.get<Roles>(`${this.apiUrl}/user/role?userId=${userId}`, { withCredentials: true });
  }

  private buildQueryString(filter: UsersFilter): string {
    const params = new URLSearchParams();
    
    if (filter.role) params.append('Role', filter.role);
    if (filter.name) params.append('Name', filter.name);
    if (filter.subjectId) params.append('SubjectId', filter.subjectId.toString());
    if (filter.schoolYear) params.append('SchoolYear', filter.schoolYear.toString());
    if (filter.page) params.append('Page', filter.page.toString());
    if (filter.perPage) params.append('PerPage', filter.perPage.toString());
    
    return params.toString();
  }
}