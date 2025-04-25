import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { SalaryFilter } from '../../shared/models/dto/analytics-dto/salary-filter.dto';
import { SalaryReportDTO } from '../../shared/models/dto/analytics-dto/salary-report.dto';
import { SalaryAnalyticsDTO } from '../../shared/models/dto/analytics-dto/salary-analytics.dto';
import { StudentAnalyticsDTO } from '../../shared/models/dto/analytics-dto/student-analytics.dto';
import { AnalyticsFilter } from '../../shared/models/dto/analytics-dto/analytics-filter.dto';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AnalyticsService {
  private httpClient = inject(HttpClient);
  private apiUrl = environment.apiUrl;

  public BasicSalaryFilter: SalaryFilter =
      {
        isSearching: false,
        teacherId: null,
        studentId: null,
    };
  
    public BasicAnalyticsFilter: AnalyticsFilter =
    {
      isSearching: false,
      teacherId: null,
      studentId: null,
      months: 1
  };
  
  getTeacherSalaryReports(filter: SalaryFilter): Observable<SalaryReportDTO[]> {
    const queryString = this.buildSalaryQueryString(filter);
    return this.httpClient.get<SalaryReportDTO[]>(`${this.apiUrl}/teacher/salary?${queryString}`, { withCredentials: true });
  }

  getStudentSalaryReports(filter: SalaryFilter): Observable<SalaryReportDTO[]> {
    const queryString = this.buildSalaryQueryString(filter);
    return this.httpClient.get<SalaryReportDTO[]>(`${this.apiUrl}/student/price?${queryString}`, { withCredentials: true });
  }

  getTeacherSalaryAnalytics(filter: AnalyticsFilter): Observable<SalaryAnalyticsDTO> {
    const queryString = this.buildAnalyticsQueryString(filter);
    return this.httpClient.get<SalaryAnalyticsDTO>(`${this.apiUrl}/teacher/salary/analytics?${queryString}`, { withCredentials: true });
  }

  getStudentPriceAnalytics(filter: AnalyticsFilter): Observable<SalaryAnalyticsDTO> {
    const queryString = this.buildAnalyticsQueryString(filter);
    return this.httpClient.get<SalaryAnalyticsDTO>(`${this.apiUrl}/student/price/analytics?${queryString}`, { withCredentials: true });
  }

  getStudentMarksAnalytics(filter: AnalyticsFilter) : Observable<StudentAnalyticsDTO> {
    const queryString = this.buildAnalyticsQueryString(filter);
    return this.httpClient.get<StudentAnalyticsDTO>(`${this.apiUrl}/student/marks/analytics?${queryString}`, { withCredentials: true });
  }

  getTeacherMarksAnalytics(filter: AnalyticsFilter): Observable<StudentAnalyticsDTO> {
    const queryString = this.buildAnalyticsQueryString(filter);
    return this.httpClient.get<StudentAnalyticsDTO>(`${this.apiUrl}/teacher/marks/analytics?${queryString}`, { withCredentials: true });
  }

  private buildSalaryQueryString(filter: SalaryFilter): string {
    const params = new URLSearchParams();
        
    if (filter.isSearching) params.append('IsSearching', "true");
    if (filter.teacherId) params.append('TeacherId', filter.teacherId.toString());
    if (filter.studentId) params.append('StudentId', filter.studentId.toString());

    return params.toString();
  }

  private buildAnalyticsQueryString(filter: AnalyticsFilter): string {
    const params = new URLSearchParams();
        
    if (filter.isSearching) params.append('IsSearching', "true");
    if (filter.teacherId) params.append('TeacherId', filter.teacherId.toString());
    if (filter.studentId) params.append('StudentId', filter.studentId.toString());
    if (filter.months) params.append('Months', filter.months.toString());

    return params.toString();
  }
}

const salaryReports: SalaryReportDTO[] = [
  {
    id: 1,
      studentFullName: "Ivan Petrenko",
      teacherFullName: "Olga Ivanova",
      lessonsCount: 12,
      price: 2400
  },
  {
    id: 2,
      studentFullName: "Anna Shevchenko",
      teacherFullName: "Serhii Bondarenko",
      lessonsCount: 10,
      price: 2000
  },
  {
    id: 3,
      studentFullName: "Oleh Marchenko",
      teacherFullName: "Iryna Dmytrenko",
      lessonsCount: 8,
      price: 1600
  },
  {
    id:4,
      studentFullName: "Kateryna Melnyk",
      teacherFullName: "Oleksandr Kharchenko",
      lessonsCount: 15,
      price: 3000
  }
];

const salaryAnalytics: SalaryAnalyticsDTO = {
  data: [523, 789, 312, 654, 982, 147, 295, 633, 412, 875, 234, 765, 911, 103, 458, 607, 830, 349, 720, 166],
  timeLabels: [
      "Day 1", "Day 2", "Day 3", "Day 4", "Day 5",
      "Day 6", "Day 7", "Day 8", "Day 9", "Day 10",
      "Day 11", "Day 12", "Day 13", "Day 14", "Day 15",
      "Day 16", "Day 17", "Day 18", "Day 19", "Day 20"
  ]
};

const studentAnalytics: StudentAnalyticsDTO = {
  timeLabels: [
    "Day 1", "Day 2", "Day 3", "Day 4", "Day 5",
    "Day 6", "Day 7", "Day 8", "Day 9", "Day 10",
    "Day 11", "Day 12", "Day 13", "Day 14", "Day 15",
    "Day 16", "Day 17", "Day 18", "Day 19", "Day 20"
  ],
  marks: [
    {
      markLabel: "Home work",
      data: [85, 90, 78, 92, 88, 76, 94, 81, 73, 89, 95, 77, 84, 91, 87, 69, 93, 82, 80, 86]
    },
    {
      markLabel: "Attention",
      data: [70, 75, 68, 80, 72, 66, 78, 73, 65, 77, 82, 71, 69, 74, 76, 67, 79, 70, 73, 75]
    },
    {
      markLabel: "Topic perception",
      data: [88, 85, 90, 86, 83, 87, 92, 89, 84, 91, 93, 88, 90, 86, 89, 87, 90, 88, 85, 91]
    }
  ]
};