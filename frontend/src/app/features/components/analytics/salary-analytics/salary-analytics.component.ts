import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { Chart, ChartConfiguration, ChartType, RadialLinearScale, registerables, scales } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { AnalyticsService } from '../../../services/analytics.service';
import { AnalyticsFilter } from '../../../../shared/models/dto/analytics-dto/analytics-filter.dto';
import { AuthService } from '../../../../core/services/auth/auth.service';
import { Roles } from '../../../../shared/models/roles.enum';
import { SalaryAnalyticsDTO } from '../../../../shared/models/dto/analytics-dto/salary-analytics.dto';
import { startWith } from 'rxjs';

Chart.register(...registerables);


@Component({
  selector: 'app-salary-analytics',
  standalone: true,
  imports: [BaseChartDirective],
  templateUrl: './salary-analytics.component.html',
  styleUrl: './salary-analytics.component.css'
})
export class SalaryAnalyticsComponent implements OnInit {
  
  private authService = inject(AuthService);
  private analyticsService = inject(AnalyticsService);
  private filter = signal<AnalyticsFilter>(this.analyticsService.BasicAnalyticsFilter);
  
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
      y: { beginAtZero: true }
    }
  };

  ngOnInit(): void {
    if (this.currentUserRole() == Roles.Student) {
      this.filter.set({ ...this.filter(), studentId: this.currentUserId() });
    }
    else {
      this.filter.set({ ...this.filter(), teacherId: this.currentUserId() });      
    }
    this.getSalaryAnalytics();
  }

  onSearch(date: string) {
    this.filter.set({ ...this.filter(), months: Number(date) });
    this.getSalaryAnalytics();
  }

  getSalaryAnalytics() {
    let result: SalaryAnalyticsDTO; 
    if (this.currentUserRole() == Roles.Student) {
      this.analyticsService.getStudentPriceAnalytics(this.filter()).subscribe({
        next: (response) => {
          result = response;
          const chart_data = [{ data: result.data, label: "salary", tension: 0.2, fill: true }];
          this.lineChartData.set({ ...this.lineChartData(), datasets: chart_data, labels: result.timeLabels });
        }
      });
    }
    else {
      this.analyticsService.getTeacherSalaryAnalytics(this.filter()).subscribe({
        next: (response) => {
          result = response;
          const chart_data = [{ data: result.data, label: "salary", tension: 0.2, fill: true }];
          this.lineChartData.set({ ...this.lineChartData(), datasets: chart_data, labels: result.timeLabels });
        }
      });
    }
  }
}
