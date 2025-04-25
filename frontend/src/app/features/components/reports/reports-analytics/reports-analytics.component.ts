import { Component, signal } from '@angular/core';
import { MarksAnalyticsComponent } from "../../analytics/marks-analytics/marks-analytics.component";
import { SalaryAnalyticsComponent } from "../../analytics/salary-analytics/salary-analytics.component";
import { SalaryReviewComponent } from "../salary-review/salary-review.component";
import { SearchUserDTO } from '../../../../shared/models/dto/reports-dto/search-user.dto';

@Component({
  selector: 'app-reports-analytics',
  standalone: true,
  imports: [MarksAnalyticsComponent, SalaryAnalyticsComponent, SalaryReviewComponent],
  templateUrl: './reports-analytics.component.html',
  styleUrl: './reports-analytics.component.css'
})
export class ReportsAnalyticsComponent {

  selectedStudentId = signal<SearchUserDTO | null>(null);

  onStudentSelect(selectedStudent: SearchUserDTO | null) {
    this.selectedStudentId.set(selectedStudent);
  }
}
