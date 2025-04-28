import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { ReportsListComponent } from "../reports-list/reports-list.component";
import { ReportsAnalyticsComponent } from "../reports-analytics/reports-analytics.component";
import { SearchUserDTO } from '../../../../shared/models/dto/reports-dto/search-user.dto';
import { ReportsService } from '../../../services/reports.service';
import { AuthService } from '../../../../core/services/auth/auth.service';
import { ReportDTO } from '../../../../shared/models/dto/reports-dto/report.dto';
import { ReportsFilter } from '../../../../shared/models/dto/reports-dto/reports-filter.dto';
import { ReportMakerComponent } from "../report-maker/report-maker.component";
import { HasRoleDirective } from '../../../../core/directives/role.directive';
import { Roles } from '../../../../shared/models/roles.enum';
import { PaginationComponent } from "../../../../shared/components/pagination/pagination.component";
import { ReportsInfoList } from '../../../../shared/models/dto/reports-dto/reports-info-list.dto';

@Component({
  selector: 'app-reports',
  standalone: true,
  imports: [
    ReportsListComponent,
    ReportsAnalyticsComponent,
    ReportMakerComponent,
    HasRoleDirective,
    PaginationComponent
],
  templateUrl: './reports.component.html',
  styleUrl: './reports.component.css'
})
export class ReportsComponent implements OnInit{

  private authService = inject(AuthService);
  private reportsService = inject(ReportsService);

  currentUserRole = computed<Roles>(() => this.authService.User()!.role);
  currentUserId = computed<number>(() => this.authService.User()!.userId);

  isStandartView = signal<boolean>(false);
  viewName = computed(() => this.isStandartView() ? "analytics" : "table");
  searchUsers = signal<SearchUserDTO[]>([]);
  reports = signal<ReportDTO[]>([]);
  filter = signal<ReportsFilter>(this.reportsService.BasicStudentsFilter);

  total = signal<number>(1);
  page = signal<number>(this.filter().page);
  limit = signal<number>(10);
  perPage = signal<number>(this.filter().perPage);
  loading = false;

  selectedReportId = signal<number | null>(null);
  reportMakerShow = signal<boolean>(false);

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
    this.getReports();
  }

  getReports() {
    let reportsInfo: ReportsInfoList = { reportsList: [], totalPageNumber: 0 };
    if (this.currentUserRole() == Roles.Student) {
      this.filter.set({ ...this.filter(), studentId: this.currentUserId() });
      this.reportsService.getStudentReports(this.filter()).subscribe({
        next: (reports) => {
          reportsInfo = reports;
          this.reports.set(reportsInfo.reportsList);
          this.total.set(reportsInfo.totalPageNumber);
        }
      })
    }
    else if (this.currentUserRole() == Roles.Teacher) {
      this.filter.set({ ...this.filter(), teacherId: this.currentUserId() });
      this.reportsService.getTeacherReports(this.filter()).subscribe({
        next: (reports) => {
          reportsInfo = reports;
          this.reports.set(reportsInfo.reportsList);
          this.total.set(reportsInfo.totalPageNumber);
        }
      })      
    }
  }

  onSwitcherChange(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.isStandartView.update(() => !this.isStandartView());
  }

  onSearch(selectedUserId: string) {
    if (selectedUserId) {
      const role = this.currentUserRole();
      if (role == Roles.Student) {
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
      this.getReports();
    }
    else {
      this.filter.set(this.reportsService.BasicStudentsFilter);
      this.getReports();
    }
  }

  onReportMakerClose() {
    this.reportMakerShow.set(false);
    this.selectedReportId.set(null);
    this.getReports();
  }

  onReportCreate() {
    this.selectedReportId.set(null);
    this.reportMakerShow.set(true);
  }

  onReportsDownload() {
  }

  onReportEdit(reportId: number) {
    this.selectedReportId.set(reportId);
    this.reportMakerShow.set(true);
  }

  goToPrevious() {
    this.page.set(this.page() - 1);
    this.filter.update((current) => ({...current, page: current.page - 1}));
    this.getReports();
  }

  goToNext() {
    this.page.set(this.page() + 1);
    this.filter.update((current) => ({...current, page: current.page + 1}));
    this.getReports();
  }

  goToPage(n: number) {
    this.page.set(n);
    this.filter.update(current => ({
      ...current,
      page: n
    }));
    this.getReports();
  }
}