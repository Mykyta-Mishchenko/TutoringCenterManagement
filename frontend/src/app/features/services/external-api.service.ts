import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ReportDTO } from '../../shared/models/dto/reports-dto/report.dto';
import { Observable } from 'rxjs';
import { Lesson } from '../../shared/models/lesson.model';
import { SalaryReportDTO } from '../../shared/models/dto/analytics-dto/salary-report.dto';

@Injectable({
  providedIn: 'root'
})
export class ExternalApiService {
  private apiUrl = environment.externalApiUrl;
  private httpClient = inject(HttpClient);

  getTeacherReports(teacherId: number) : Observable<ReportDTO[]> {
    return this.httpClient.get<ReportDTO[]>(`${this.apiUrl}/download/teacher/reports/${teacherId}`, { withCredentials: true });
  }

  getTeacherSchedule(teacherId: number) : Observable<Lesson[]> {
    return this.httpClient.get<Lesson[]>(`${this.apiUrl}/download/teacher/schedule/${teacherId}`, { withCredentials: true });
  }

  getTeacherSalaryReports(teacherId: number) : Observable<SalaryReportDTO[]> {
    return this.httpClient.get<SalaryReportDTO[]>(`${this.apiUrl}/download/teacher/salary-reports/${teacherId}`, { withCredentials: true });
  }
}
