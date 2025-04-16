import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ReportDTO } from '../../shared/models/dto/reports-dto/report.dto';
import { MarkDTO } from '../../shared/models/dto/reports-dto/mark.dto';
import { MarkType } from '../../shared/models/mark-type.model';
import { SearchUserDTO } from '../../shared/models/dto/reports-dto/search-user.dto';
import { StudentsFilter } from '../../shared/models/dto/reports-dto/students-filter.dto';
import { ReportCreatingDTO } from '../../shared/models/dto/reports-dto/report-creating.dto';
import { ReportEditingDTO } from '../../shared/models/dto/reports-dto/report-editing.dto';
import { ReportsInfoList } from '../../shared/models/dto/reports-dto/reports-info-list.dto';

@Injectable({
  providedIn: 'root'
})
export class ReportsService {
  private apiUrl = environment.apiUrl;
  private httpClient = inject(HttpClient);

  public BasicStudentsFilter: StudentsFilter =
    {
      teacherId: null,
      studentId: null,
      page: 1,
      perPage: 20
    };

  getUserReports(filter: StudentsFilter): ReportsInfoList {
    return { totalPageNumber: 4, reportsList: reports };
  }

  getMarkTypes(): MarkType[] {
    return markTypes;
  }

  addUserReport(report: ReportCreatingDTO) {
    
  }

  editReport(report : ReportEditingDTO) {
    
  }

  getReport(reportId: number): ReportDTO{
    return {
      reportId: 1,
      studentId: 1,
      teacherFullName: "Anna Kovalenko",
      studentFullName: "Ivan Petrov",
      description: "Ivan shows consistent improvement in algebra and geometry.",
      date: new Date("2025-04-01"),
      marks: [
        { markTypeId: 1, markValue: 8 },
        { markTypeId: 2, markValue: 9 },
        { markTypeId: 3, markValue: 7 }
      ]
    };
  }

  getTeacherStudents(teacherId: number): SearchUserDTO[] {
    return students;
  }

  getStudentTeachers(studentId: number): SearchUserDTO[]{
    return teachers;
  }

}

const students: SearchUserDTO[] = [
  { userId: 1, fullName: "Ivan Petrov" },
  { userId: 2, fullName: "Maria Ivanova" },
  { userId: 3, fullName: "Sofiia Melnyk" }
]

const teachers: SearchUserDTO[] = [
  { userId: 1, fullName: "Anna Kovalenko" },
  { userId: 2, fullName: "Olga Shevchenko" },
  { userId: 3, fullName: "Dmytro Bondarenko" }
]

const markTypes: MarkType[] = [
  { typeId: 1, name: 'Home work score' },
  { typeId: 2, name: 'Attention score' },
  { typeId: 3, name: 'Topic perception score' }
]

const reports: ReportDTO[] = [
  {
    reportId: 1,
    studentId: 1,
    teacherFullName: "Anna Kovalenko",
    studentFullName: "Ivan Petrov",
    description: "Ivan shows consistent improvement in algebra and geometry.",
    date: new Date("2025-04-01"),
    marks: [
      { markTypeId: 1, markValue: 8 },
      { markTypeId: 2, markValue: 9 },
      { markTypeId: 3, markValue: 7 }
    ]
  },
  {
    reportId: 2,
    studentId: 2,
    teacherFullName: "Olga Shevchenko",
    studentFullName: "Maria Ivanova",
    description: "Maria actively participates in class discussions.",
    date: new Date("2025-04-02"),
    marks: [
      { markTypeId: 1, markValue: 8 },
      { markTypeId: 2, markValue: 9 },
      { markTypeId: 3, markValue: 7 }
    ]
  },
  {
    reportId: 3,
    studentId: 3,
    teacherFullName: "Dmytro Bondarenko",
    studentFullName: "Oleksiy Koval",
    description: "Oleksiy needs to focus more on homework assignments.",
    date: new Date("2025-04-03"),
    marks: [
      { markTypeId: 1, markValue: 8 },
      { markTypeId: 2, markValue: 9 },
      { markTypeId: 3, markValue: 7 }
    ]
  },
  {
    reportId: 4,
    studentId: 4,
    teacherFullName: "Natalia Lysenko",
    studentFullName: "Sofiia Melnyk",
    description: "Excellent progress in reading comprehension and writing.",
    date: new Date("2025-04-04"),
    marks: [
      { markTypeId: 1, markValue: 8 },
      { markTypeId: 2, markValue: 9 },
      { markTypeId: 3, markValue: 7 }
    ]
  },
  {
    reportId: 5,
    studentId: 5,
    teacherFullName: "Serhiy Kravchenko",
    studentFullName: "Danylo Tkachenko",
    description: "Danylo is very creative and delivers quality work.",
    date: new Date("2025-04-05"),
    marks: [
      { markTypeId: 1, markValue: 8 },
      { markTypeId: 2, markValue: 9 },
      { markTypeId: 3, markValue: 7 }
    ]
  },
  {
    reportId: 6,
    studentId: 6,
    teacherFullName: "Yuliia Moroz",
    studentFullName: "Kateryna Bondar",
    description: "Good effort but needs to improve punctuality.",
    date: new Date("2025-04-06"),
    marks: [
      { markTypeId: 1, markValue: 8 },
      { markTypeId: 2, markValue: 9 },
      { markTypeId: 3, markValue: 7 }
    ]
  },
  {
    reportId: 7,
    studentId: 7,
    teacherFullName: "Andrii Tkachenko",
    studentFullName: "Artem Dmytrenko",
    description: "Artem has made significant improvement in math.",
    date: new Date("2025-04-07"),
    marks: [
      { markTypeId: 1, markValue: 8 },
      { markTypeId: 2, markValue: 9 },
      { markTypeId: 3, markValue: 7 }
    ]
  },
  {
    reportId: 8,
    studentId: 8,
    teacherFullName: "Iryna Sydorenko",
    studentFullName: "Valeriia Holub",
    description: "Valeriia consistently performs well in group projects.",
    date: new Date("2025-04-08"),
    marks: [
      { markTypeId: 1, markValue: 8 },
      { markTypeId: 2, markValue: 9 },
      { markTypeId: 3, markValue: 7 }
    ]
  },
  {
    reportId: 9,
    studentId: 9,
    teacherFullName: "Oleh Romanenko",
    studentFullName: "Denys Karpov",
    description: "Denys needs to participate more actively in class.",
    date: new Date("2025-04-09"),
    marks: [
      { markTypeId: 1, markValue: 8 },
      { markTypeId: 2, markValue: 9 },
      { markTypeId: 3, markValue: 7 }
    ]
  },
  {
    reportId: 10,
    studentId: 10,
    teacherFullName: "Tetiana Matviyenko",
    studentFullName: "Yevhen Pavlenko",
    description: "Yevhen is a quick learner and very inquisitive.",
    date: new Date("2025-04-10"),
    marks: [
      { markTypeId: 1, markValue: 8 },
      { markTypeId: 2, markValue: 9 },
      { markTypeId: 3, markValue: 7 }
    ]
  },
  {
    reportId: 11,
    studentId: 11,
    teacherFullName: "Bohdan Shulha",
    studentFullName: "Oleksandra Nazarenko",
    description: "Very analytical thinker with great attention to detail.",
    date: new Date("2025-04-11"),
    marks: [
      { markTypeId: 1, markValue: 8 },
      { markTypeId: 2, markValue: 9 },
      { markTypeId: 3, markValue: 7 }
    ]
  },
  {
    reportId: 12,
    studentId: 12,
    teacherFullName: "Larysa Petrenko",
    studentFullName: "Nazar Myronenko",
    description: "Nazar often needs extra help with grammar.",
    date: new Date("2025-04-12"),
    marks: [
      { markTypeId: 1, markValue: 8 },
      { markTypeId: 2, markValue: 9 },
      { markTypeId: 3, markValue: 7 }
    ]
  },
  {
    reportId: 13,
    studentId: 13,
    teacherFullName: "Yurii Kovalchuk",
    studentFullName: "Anastasiia Vovk",
    description: "Anastasiia is a leader and helps her peers.",
    date: new Date("2025-04-13"),
    marks: [
      { markTypeId: 1, markValue: 8 },
      { markTypeId: 2, markValue: 9 },
      { markTypeId: 3, markValue: 7 }
    ]
  },
  {
    reportId: 14,
    studentId: 14,
    teacherFullName: "Viktoriia Hnatenko",
    studentFullName: "Roman Yurchenko",
    description: "Roman is diligent and consistent in his studies.",
    date: new Date("2025-04-14"),
    marks: [
      { markTypeId: 1, markValue: 8 },
      { markTypeId: 2, markValue: 9 },
      { markTypeId: 3, markValue: 7 }
    ]
  },
  {
    reportId: 15,
    studentId: 15,
    teacherFullName: "Ihor Polishchuk",
    studentFullName: "Liliia Martynenko",
    description: "Liliia is very artistic and excels in creative subjects.",
    date: new Date("2025-04-15"),
    marks: [
      { markTypeId: 1, markValue: 8 },
      { markTypeId: 2, markValue: 9 },
      { markTypeId: 3, markValue: 7 }
    ]
  },
  {
    reportId: 16,
    studentId: 16,
    teacherFullName: "Olena Danylenko",
    studentFullName: "Marko Semenov",
    description: "Marko needs to focus more during lessons.",
    date: new Date("2025-04-16"),
    marks: [
      { markTypeId: 1, markValue: 8 },
      { markTypeId: 2, markValue: 9 },
      { markTypeId: 3, markValue: 7 }
    ]
  },
  {
    reportId: 17,
    studentId: 17,
    teacherFullName: "Vadym Horobets",
    studentFullName: "Tetiana Kyrylenko",
    description: "Very positive attitude and always punctual.",
    date: new Date("2025-04-17"),
    marks: [
      { markTypeId: 1, markValue: 8 },
      { markTypeId: 2, markValue: 9 },
      { markTypeId: 3, markValue: 7 }
    ]
  },
  {
    reportId: 18,
    studentId: 18,
    teacherFullName: "Kseniia Melnychuk",
    studentFullName: "Andrii Hrytsenko",
    description: "Shows great leadership and teamwork skills.",
    date: new Date("2025-04-18"),
    marks: [
      { markTypeId: 1, markValue: 8 },
      { markTypeId: 2, markValue: 9 },
      { markTypeId: 3, markValue: 7 }
    ]
  },
  {
    reportId: 19,
    studentId: 19,
    teacherFullName: "Volodymyr Zaitsev",
    studentFullName: "Alina Chornovol",
    description: "Needs to improve time management for assignments.",
    date: new Date("2025-04-19"),
    marks: [
      { markTypeId: 1, markValue: 8 },
      { markTypeId: 2, markValue: 9 },
      { markTypeId: 3, markValue: 7 }
    ]
  },
  {
    reportId: 20,
    studentId: 20,
    teacherFullName: "Mariia Soroka",
    studentFullName: "Bohdan Lisovyi",
    description: "Bohdan is a bright student and asks thoughtful questions.",
    date: new Date("2025-04-20"),
    marks: [
      { markTypeId: 1, markValue: 8 },
      { markTypeId: 2, markValue: 9 },
      { markTypeId: 3, markValue: 7 }
    ]
  }
];
