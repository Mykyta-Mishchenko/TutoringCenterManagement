import { Injectable, OnInit} from "@angular/core";
import { Teacher } from "../../shared/models/teacher.model";
import { Schedule } from "../../shared/models/schedule.model";
import { User } from "../../shared/models/user.models";
import { Roles } from "../../shared/models/roles.enum";
import { UsersFilter } from "../../shared/models/dto/users-filter.dto";

@Injectable({
    providedIn: 'root'
})
export class UsersService implements OnInit{

  public BasicTeachersFilter: UsersFilter =
  {
    role: Roles.Teacher,
    name: null,
    subjectId: null,
    schoolYear: null,
    page: 1,
    perPage: 20
  };

  ngOnInit(): void {
    
  }

  getUser(userId: number) {
    return {} as User;
  }

  getUsersByFilter(filter: UsersFilter) {
    return SAMPLE_TEACHERS_LIST;
  }

  getUserRole(userId: number): Roles {
    return Roles.Teacher;
  }
}

export const SAMPLE_TEACHERS_LIST: Teacher[] = [
  {
    id: 1,
    fullName: "Olena Sydorenko",
    profileImgUrl: null,
    lessons: [
      {
        lessonId: 0,
        teacherId: 1,
        studentsCount: 2,
        type: { typeId: 1, maxStudentsCount: 1, subject: { subjectId: 1, name: "Math" }, schoolYear: 7, price: 300 },
        schedule: {} as Schedule
      },
      {
        lessonId: 1,
        teacherId: 1,
        studentsCount: 1,
        type: { typeId: 1, maxStudentsCount: 1, subject: { subjectId: 1, name: "Math" }, schoolYear: 5, price: 200 },
        schedule: {} as Schedule
      },
      {
        lessonId: 2,
        teacherId: 1,
        studentsCount: 3,
        type: { typeId: 2, maxStudentsCount: 5, subject: { subjectId: 2, name: "Physics" }, schoolYear: 6, price: 180 },
        schedule: {} as Schedule
      }
    ]
  },
  {
    id: 2,
    fullName: "Andriy Tkachenko",
    profileImgUrl: null,
    lessons: [
      {
        lessonId: 3,
        teacherId: 2,
        studentsCount: 4,
        type: { typeId: 3, maxStudentsCount: 5, subject: { subjectId: 3, name: "Biology" }, schoolYear: 8, price: 170 },
        schedule: {} as Schedule
      },
      {
        lessonId: 4,
        teacherId: 2,
        studentsCount: 2,
        type: { typeId: 4, maxStudentsCount: 5, subject: { subjectId: 1, name: "Math" }, schoolYear: 7, price: 160 },
        schedule: {} as Schedule
      }
    ]
  },
  {
    id: 3,
    fullName: "Iryna Koval",
    profileImgUrl: null,
    lessons: [
      {
        lessonId: 5,
        teacherId: 3,
        studentsCount: 1,
        type: { typeId: 5, maxStudentsCount: 1, subject: { subjectId: 4, name: "English" }, schoolYear: 4, price: 220 },
        schedule: {} as Schedule
      }
    ]
  },
  {
    id: 4,
    fullName: "Serhii Petrenko",
    profileImgUrl: null,
    lessons: [
      {
        lessonId: 6,
        teacherId: 4,
        studentsCount: 5,
        type: { typeId: 6, maxStudentsCount: 5, subject: { subjectId: 5, name: "Chemistry" }, schoolYear: 9, price: 190 },
        schedule: {} as Schedule
      }
    ]
  },
  {
    id: 5,
    fullName: "Natalia Bondarenko",
    profileImgUrl: null,
    lessons: [
      {
        lessonId: 7,
        teacherId: 5,
        studentsCount: 2,
        type: { typeId: 7, maxStudentsCount: 5, subject: { subjectId: 2, name: "Physics" }, schoolYear: 10, price: 180 },
        schedule: {} as Schedule
      }
    ]
  },
  {
    id: 6,
    fullName: "Volodymyr Kravchenko",
    profileImgUrl: null,
    lessons: [
      {
        lessonId: 8,
        teacherId: 6,
        studentsCount: 1,
        type: { typeId: 8, maxStudentsCount: 1, subject: { subjectId: 6, name: "Geography" }, schoolYear: 6, price: 200 },
        schedule: {} as Schedule
      }
    ]
  },
  {
    id: 7,
    fullName: "Oksana Lysenko",
    profileImgUrl: null,
    lessons: [
      {
        lessonId: 9,
        teacherId: 7,
        studentsCount: 3,
        type: { typeId: 9, maxStudentsCount: 5, subject: { subjectId: 7, name: "History" }, schoolYear: 7, price: 170 },
        schedule: {} as Schedule
      }
    ]
  },
  {
    id: 8,
    fullName: "Ivan Moroz",
    profileImgUrl: null,
    lessons: [
      {
        lessonId: 10,
        teacherId: 8,
        studentsCount: 4,
        type: { typeId: 10, maxStudentsCount: 5, subject: { subjectId: 3, name: "Biology" }, schoolYear: 11, price: 190 },
        schedule: {} as Schedule
      }
    ]
  },
  {
    id: 9,
    fullName: "Tetiana Marchenko",
    profileImgUrl: null,
    lessons: [
      {
        lessonId: 11,
        teacherId: 9,
        studentsCount: 2,
        type: { typeId: 11, maxStudentsCount: 5, subject: { subjectId: 8, name: "Literature" }, schoolYear: 5, price: 180 },
        schedule: {} as Schedule
      }
    ]
  },
  {
    id: 10,
    fullName: "Roman Shapoval",
    profileImgUrl: null,
    lessons: [
      {
        lessonId: 12,
        teacherId: 10,
        studentsCount: 5,
        type: { typeId: 12, maxStudentsCount: 5, subject: { subjectId: 9, name: "Informatics" }, schoolYear: 12, price: 210 },
        schedule: {} as Schedule
      }
    ]
  },
  {
    id: 11,
    fullName: "Larysa Horobets",
    profileImgUrl: null,
    lessons: [
      {
        lessonId: 13,
        teacherId: 11,
        studentsCount: 1,
        type: { typeId: 13, maxStudentsCount: 1, subject: { subjectId: 10, name: "Art" }, schoolYear: 3, price: 160 },
        schedule: {} as Schedule
      }
    ]
  },
  {
    id: 12,
    fullName: "Petro Zadorozhnyi",
    profileImgUrl: null,
    lessons: [
      {
        lessonId: 14,
        teacherId: 12,
        studentsCount: 4,
        type: { typeId: 14, maxStudentsCount: 5, subject: { subjectId: 11, name: "Music" }, schoolYear: 4, price: 150 },
        schedule: {} as Schedule
      }
    ]
  },
  {
    id: 13,
    fullName: "Halyna Tarasova",
    profileImgUrl: null,
    lessons: [
      {
        lessonId: 15,
        teacherId: 13,
        studentsCount: 3,
        type: { typeId: 15, maxStudentsCount: 5, subject: { subjectId: 4, name: "English" }, schoolYear: 6, price: 190 },
        schedule: {} as Schedule
      }
    ]
  },
  {
    id: 14,
    fullName: "Maksym Vovk",
    profileImgUrl: null,
    lessons: [
      {
        lessonId: 16,
        teacherId: 14,
        studentsCount: 2,
        type: { typeId: 16, maxStudentsCount: 5, subject: { subjectId: 2, name: "Physics" }, schoolYear: 8, price: 200 },
        schedule: {} as Schedule
      }
    ]
  },
  {
    id: 15,
    fullName: "Kateryna Danyliuk",
    profileImgUrl: null,
    lessons: [
      {
        lessonId: 17,
        teacherId: 15,
        studentsCount: 5,
        type: { typeId: 17, maxStudentsCount: 5, subject: { subjectId: 5, name: "Chemistry" }, schoolYear: 10, price: 210 },
        schedule: {} as Schedule
      }
    ]
  },
  {
    id: 16,
    fullName: "Bohdan Yurchenko",
    profileImgUrl: null,
    lessons: [
      {
        lessonId: 18,
        teacherId: 16,
        studentsCount: 3,
        type: { typeId: 18, maxStudentsCount: 5, subject: { subjectId: 6, name: "Geography" }, schoolYear: 9, price: 180 },
        schedule: {} as Schedule
      }
    ]
  },
  {
    id: 17,
    fullName: "Sofiia Lytvyn",
    profileImgUrl: null,
    lessons: [
      {
        lessonId: 19,
        teacherId: 17,
        studentsCount: 2,
        type: { typeId: 19, maxStudentsCount: 5, subject: { subjectId: 1, name: "Math" }, schoolYear: 12, price: 200 },
        schedule: {} as Schedule
      }
    ]
  },
  {
    id: 18,
    fullName: "Oleksandr Melnyk",
    profileImgUrl: null,
    lessons: [
      {
        lessonId: 20,
        teacherId: 18,
        studentsCount: 1,
        type: { typeId: 20, maxStudentsCount: 1, subject: { subjectId: 7, name: "History" }, schoolYear: 3, price: 160 },
        schedule: {} as Schedule
      }
    ]
  },
  {
    id: 19,
    fullName: "Yuliia Panchenko",
    profileImgUrl: null,
    lessons: [
      {
        lessonId: 21,
        teacherId: 19,
        studentsCount: 2,
        type: { typeId: 21, maxStudentsCount:5, subject: { subjectId: 9, name: "Informatics" }, schoolYear: 11, price: 210 },
        schedule: {} as Schedule
      }
    ]
  },
  {
    id: 20,
    fullName: "Denys Shevchuk",
    profileImgUrl: null,
    lessons: [
      {
        lessonId: 22,
        teacherId: 20,
        studentsCount: 4,
        type: { typeId: 22, maxStudentsCount: 5, subject: { subjectId: 8, name: "Literature" }, schoolYear: 7, price: 190 },
        schedule: {} as Schedule
      }
    ]
  }
];