import { Component, computed, inject, input, OnChanges, OnInit, signal, SimpleChanges } from '@angular/core';
import { Chart, ChartConfiguration, ChartType, registerables } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { AnalyticsService } from '../../../services/analytics.service';
import { MarkType } from '../../../../shared/models/mark-type.model';
import { SearchUserDTO } from '../../../../shared/models/dto/reports-dto/search-user.dto';
import { AuthService } from '../../../../core/services/auth/auth.service';
import { Roles } from '../../../../shared/models/roles.enum';
import { StudentAnalyticsDTO } from '../../../../shared/models/dto/analytics-dto/student-analytics.dto';

Chart.register(...registerables);

@Component({
  selector: 'app-marks-analytics',
  standalone: true,
  imports: [BaseChartDirective],
  templateUrl: './marks-analytics.component.html',
  styleUrl: './marks-analytics.component.css'
})
export class MarksAnalyticsComponent implements OnInit, OnChanges {
  selectedStudent = input.required<SearchUserDTO | null>();

  private authService = inject(AuthService);
  private analyticsService = inject(AnalyticsService);
  private filter = this.analyticsService.BasicAnalyticsFilter;
  
  currentUserRole = computed<Roles>(() => this.authService.User()!.role);
  currentUserId = computed<number>(() => this.authService.User()!.userId);

  lineChartData = signal<ChartConfiguration<'line'>['data']>({
    labels: [],
    datasets: [],
  });
  lineChartType : ChartType= 'line';
  lineChartOptions: any = {
    responsive: true,
    maintainAspectRatio: false,
    scales: {
      y: {
        beginAtZero: true,
        max: 10
       }
    }
  };
  
  markTypes = signal<MarkType[]>([]);
  
  ngOnInit(): void {
    if (this.currentUserRole() == Roles.Student) {
      this.filter.studentId = this.currentUserId();
    }
    else {
      this.filter.teacherId = this.currentUserId();
    }

    this.getAnalytics();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (this.selectedStudent()) {
      this.filter.isSearching = true;
      if (this.currentUserRole() == Roles.Student) {
        this.filter.teacherId = this.selectedStudent()!.userId;
      }
      else {
        this.filter.studentId = this.selectedStudent()!.userId;
      }
    }
    else if (this.filter.isSearching) {
      this.filter.isSearching = false;
    }
    this.getAnalytics();
  }

  onSearch(months: string) {
    this.filter.months = Number(months);
    this.getAnalytics();
  }

  getAnalytics() {
    if (this.currentUserRole() == Roles.Student) {
      this.filter.studentId = this.currentUserId();
      this.analyticsService.getStudentMarksAnalytics(this.filter).subscribe({
        next: (response) => {
          this.getLineChartData(response);
        }
      });
    }
    else {
      this.filter.teacherId = this.currentUserId();
      this.analyticsService.getTeacherMarksAnalytics(this.filter).subscribe({
        next: (response) => {
          this.getLineChartData(response);
        }
      });
    }
  }

  getLineChartData(result: StudentAnalyticsDTO) {
    let marks = [];
    for (let mark of result.marks) {
      const chart_data = { data: mark.data, label: mark.markLabel, tension: 0.2, fill: true };  
      marks.push(chart_data);
    }
    
    this.lineChartData.set({ datasets: marks, labels: result.timeLabels });
  }
}
