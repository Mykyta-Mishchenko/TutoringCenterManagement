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
export class UsersService implements OnInit{

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

  ngOnInit(): void {
    
  }

  getUser(userId: number) {
    return {} as User;
  }

  getUsersByFilter(filter: UsersFilter): Observable<UsersInfoList> {
    const queryString = this.buildQueryString(filter);
    return this.httpClient.get<UsersInfoList>(`${this.apiUrl}/users?${queryString}`, { withCredentials: true });
  }

  getUserRole(userId: number): Roles {
    return Roles.Teacher;
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

export const SAMPLE_TEACHERS_LIST: UserInfo[] = [
  {
    userId: 1,
    fullName: "Olena Sydorenko",
    subjects : [
      { name: "Math", minSchoolYear: 5, maxSchoolYear: 7 },
      { name: "Physics", minSchoolYear: 6, maxSchoolYear: 6 }
    ]
  },
  {
    userId: 2,
    fullName: "Andriy Tkachenko",
    subjects: [
      { name: "Biology", minSchoolYear: 8, maxSchoolYear: 8 },
      { name: "Math", minSchoolYear: 7, maxSchoolYear: 7 }
    ]
  },
  {
    userId: 3,
    fullName: "Iryna Koval",
    subjects: [
      { name: "English", minSchoolYear: 4, maxSchoolYear: 4 }
    ]
  },
  {
    userId: 4,
    fullName: "Serhii Petrenko",
    subjects: [
      { name: "Chemistry", minSchoolYear: 9, maxSchoolYear: 9 }
    ]
  },
  {
    userId: 5,
    fullName: "Natalia Bondarenko",
    subjects: [
      { name: "Physics", minSchoolYear: 10, maxSchoolYear: 10 }
    ]
  },
  {
    userId: 6,
    fullName: "Volodymyr Kravchenko",
    subjects: [
      { name: "Geography", minSchoolYear: 6, maxSchoolYear: 6 }
    ]
  },
  {
    userId: 7,
    fullName: "Oksana Lysenko",
    subjects: [
      { name: "History", minSchoolYear: 7, maxSchoolYear: 7 }
    ]
  },
  {
    userId: 8,
    fullName: "Ivan Moroz",
    subjects: [
      { name: "Biology", minSchoolYear: 11, maxSchoolYear: 11 }
    ]
  },
  {
    userId: 9,
    fullName: "Tetiana Marchenko",
    subjects: [
      { name: "Literature", minSchoolYear: 5, maxSchoolYear: 5 }
    ]
  },
  {
    userId: 10,
    fullName: "Roman Shapoval",
    subjects: [
      { name: "Informatics", minSchoolYear: 12, maxSchoolYear: 12 }
    ]
  },
  {
    userId: 11,
    fullName: "Larysa Horobets",
    subjects: [
      { name: "Art", minSchoolYear: 3, maxSchoolYear: 3 }
    ]
  },
  {
    userId: 12,
    fullName: "Petro Zadorozhnyi",
    subjects: [
      { name: "Music", minSchoolYear: 4, maxSchoolYear: 4 }
    ]
  },
  {
    userId: 13,
    fullName: "Halyna Tarasova",
    subjects: [
      { name: "English", minSchoolYear: 6, maxSchoolYear: 6 }
    ]
  },
  {
    userId: 14,
    fullName: "Maksym Vovk",
    subjects: [
      { name: "Physics", minSchoolYear: 8, maxSchoolYear: 8 }
    ]
  },
  {
    userId: 15,
    fullName: "Kateryna Danyliuk",
    subjects: [
      { name: "Chemistry", minSchoolYear: 10, maxSchoolYear: 10 }
    ]
  },
  {
    userId: 16,
    fullName: "Bohdan Yurchenko",
    subjects: [
      { name: "Geography", minSchoolYear: 9, maxSchoolYear: 9 }
    ]
  },
  {
    userId: 17,
    fullName: "Sofiia Lytvyn",
    subjects: [
      { name: "Math", minSchoolYear: 12, maxSchoolYear: 12 }
    ]
  },
  {
    userId: 18,
    fullName: "Oleksandr Melnyk",
    subjects: [
      { name: "History", minSchoolYear: 3, maxSchoolYear: 3 }
    ]
  },
  {
    userId: 19,
    fullName: "Yuliia Panchenko",
    subjects: [
      { name: "Informatics", minSchoolYear: 11, maxSchoolYear: 11 }
    ]
  },
  {
    userId: 20,
    fullName: "Denys Shevchuk",
    subjects: [
      { name: "Literature", minSchoolYear: 7, maxSchoolYear: 7 }
    ]
  }
];