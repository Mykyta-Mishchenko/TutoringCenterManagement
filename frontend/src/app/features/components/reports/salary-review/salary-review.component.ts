import { Component, computed, inject, OnInit, output, signal } from '@angular/core';
import { SearchUserDTO } from '../../../../shared/models/dto/reports-dto/search-user.dto';
import { ReportsService } from '../../../services/reports.service';
import { Roles } from '../../../../shared/models/roles.enum';
import { AuthService } from '../../../../core/services/auth/auth.service';
import { HasRoleDirective } from '../../../../core/directives/role.directive';
import { AnalyticsService } from '../../../services/analytics.service';
import { SalaryFilter } from '../../../../shared/models/dto/analytics-dto/salary-filter.dto';
import { SalaryReportDTO } from '../../../../shared/models/dto/analytics-dto/salary-report.dto';
import { ExternalApiService } from '../../../services/external-api.service';

@Component({
  selector: 'app-salary-review',
  standalone: true,
  imports: [HasRoleDirective],
  templateUrl: './salary-review.component.html',
  styleUrl: './salary-review.component.css'
})
export class SalaryReviewComponent implements OnInit{
  
  private analyticsService = inject(AnalyticsService);
  private reportsService = inject(ReportsService);
  private authService = inject(AuthService);
  private externalApiService = inject(ExternalApiService);

  selectedStudent = output<SearchUserDTO | null>();

  currentUserRole = computed<Roles>(() => this.authService.User()!.role);
  currentUserId = computed<number>(() => this.authService.User()!.userId);

  searchUsers = signal<SearchUserDTO[]>([]);
  salaryReports = signal<SalaryReportDTO[]>([]);
  totalPrice = signal<number>(0);
  totalLessonsCount = signal<number>(0);

  filter = signal<SalaryFilter>(this.analyticsService.BasicSalaryFilter);

  ngOnInit(): void {
    if (this.currentUserRole() == Roles.Student) {
      this.reportsService.getStudentTeachers(this.currentUserId()).subscribe({
        next: (teachers) => {
          this.searchUsers.set(teachers);
        }
      })
    }
    else {
      this.reportsService.getTeacherStudents(this.currentUserId()).subscribe({
        next: (students) => {
          this.searchUsers.set(students);
        }
      })
    }
    this.getSalaryReports();
  }

  onSearch(selectedUserId: string) {
    if (selectedUserId) {
      const user = this.searchUsers().find(u => u.userId == Number(selectedUserId)) ?? null;
      this.selectedStudent.emit(user);
      if (this.currentUserRole() == Roles.Student) {
        this.filter.set({
          ...this.filter(),
          isSearching: true,
          teacherId: Number(selectedUserId)
        });
      }
      else{
        this.filter.set({
          ...this.filter(),
          isSearching: true,
          studentId: Number(selectedUserId)
        });
      }
      this.getSalaryReports();
    }
    else {
      this.selectedStudent.emit(null);
      this.filter.set(this.analyticsService.BasicSalaryFilter);
      this.getSalaryReports();
    }
  }

  getSalaryReports() {
    if (this.currentUserRole() == Roles.Student) {
      this.filter.set({ ...this.filter(), studentId: this.currentUserId() });
      this.analyticsService.getStudentSalaryReports(this.filter()).subscribe({
        next: (response) => {
          this.salaryReports.set(response);
          this.totalPrice.set(this.salaryReports().reduce((sum, report) => sum + report.price, 0));
          this.totalLessonsCount.set(this.salaryReports().reduce((sum, report) => sum + report.lessonsCount, 0));
        }
      })
    }
    else if (this.currentUserRole() == Roles.Teacher) {
      this.filter.set({ ...this.filter(), teacherId: this.currentUserId() });
      this.analyticsService.getTeacherSalaryReports(this.filter()).subscribe({
        next: (response) => {
          this.salaryReports.set(response);
          this.totalPrice.set(this.salaryReports().reduce((sum, report) => sum + report.price, 0));
          this.totalLessonsCount.set(this.salaryReports().reduce((sum, report) => sum + report.lessonsCount, 0));
        }
      })
    }
  }
  onSalaryDownload() {
    this.externalApiService.getTeacherSalaryReports(this.currentUserId()).subscribe({
      next: (reports) => {

        const blob = new Blob([JSON.stringify(reports,null,2)], { type: 'text/plain;charset=utf-8' });
        const url = window.URL.createObjectURL(blob);

        const link = document.createElement('a');
        link.href = url;
        link.download = 'reports.json';
        link.click();

        window.URL.revokeObjectURL(url);
      }
    })
  }
}
