import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ReportDTO } from '../../shared/models/dto/reports-dto/report.dto';
import { MarkType } from '../../shared/models/mark-type.model';
import { SearchUserDTO } from '../../shared/models/dto/reports-dto/search-user.dto';
import { ReportsFilter } from '../../shared/models/dto/reports-dto/reports-filter.dto';
import { ReportCreatingDTO } from '../../shared/models/dto/reports-dto/report-creating.dto';
import { ReportEditingDTO } from '../../shared/models/dto/reports-dto/report-editing.dto';
import { ReportsInfoList } from '../../shared/models/dto/reports-dto/reports-info-list.dto';
import { Observable } from 'rxjs';
import { ReportScheduleDTO } from '../../shared/models/dto/reports-dto/lesson-schedule.dto';

@Injectable({
  providedIn: 'root'
})
export class ReportsService {
  private apiUrl = environment.apiUrl;
  private httpClient = inject(HttpClient);

  public BasicStudentsFilter: ReportsFilter =
    {
      isSearching: false,
      teacherId: null,
      studentId: null,
      page: 1,
      perPage: 20
    };
  
  getTeacherReports(filter: ReportsFilter): Observable<ReportsInfoList> {
    const queryString = this.buildQueryString(filter);
    return this.httpClient.get<ReportsInfoList>(`${this.apiUrl}/teacher/reports?${queryString}`, { withCredentials: true });
  }

  getStudentReports(filter: ReportsFilter): Observable<ReportsInfoList> {
    const queryString = this.buildQueryString(filter);
    return this.httpClient.get<ReportsInfoList>(`${this.apiUrl}/student/reports?${queryString}`, { withCredentials: true });
  }

  getMarkTypes(): Observable<MarkType[]> {
    return this.httpClient.get<MarkType[]>(`${this.apiUrl}/marks/types`, { withCredentials: true });
  }

  addReport(report: ReportCreatingDTO) : Observable<any> {
    return this.httpClient.post(`${this.apiUrl}/reports/add`, report, { withCredentials: true });
  }

  editReport(report : ReportEditingDTO) : Observable<any>{
    return this.httpClient.put(`${this.apiUrl}/reports/edit`, report, { withCredentials: true });
  }

  getStudentSchedule(teacherId: number, studentId: number): Observable<ReportScheduleDTO[]> {
    return this.httpClient.get<ReportScheduleDTO[]>(
      `${this.apiUrl}/teacher/students/reports/empty?teacherId=${teacherId}&studentId=${studentId}`,
      { withCredentials: true });
  }

  getReport(reportId: number): Observable<ReportDTO>{
    return this.httpClient.get<ReportDTO>(`${this.apiUrl}/reports?reportId=${reportId}`, { withCredentials: true });
  }

  getTeacherStudents(teacherId: number): Observable<SearchUserDTO[]> {
    return this.httpClient.get<SearchUserDTO[]>(`${this.apiUrl}/teacher/students?teacherId=${teacherId}`, { withCredentials: true });
  }

  getStudentTeachers(studentId: number): Observable<SearchUserDTO[]>{
    return this.httpClient.get<SearchUserDTO[]>(`${this.apiUrl}/student/teachers?studentId=${studentId}`, { withCredentials: true });
  }

  private buildQueryString(filter: ReportsFilter): string {
      const params = new URLSearchParams();
      
      if (filter.isSearching) params.append('IsSearching', "true");
      if (filter.teacherId) params.append('TeacherId', filter.teacherId.toString());
      if (filter.studentId) params.append('StudentId', filter.studentId.toString());
      if (filter.page) params.append('Page', filter.page.toString());
      if (filter.perPage) params.append('PerPage', filter.perPage.toString());
      
      return params.toString();
    }

}