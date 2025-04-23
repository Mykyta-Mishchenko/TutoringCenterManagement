import { Component, inject, input, OnInit, output, signal } from '@angular/core';
import { ReportDTO } from '../../../../shared/models/dto/reports-dto/report.dto';
import { ReportsService } from '../../../services/reports.service';
import { DatePipe } from '@angular/common';
import { HasRoleDirective } from '../../../../core/directives/role.directive';
import { MarkType } from '../../../../shared/models/mark-type.model';

@Component({
  selector: 'app-reports-list',
  standalone: true,
  imports: [DatePipe, HasRoleDirective],
  templateUrl: './reports-list.component.html',
  styleUrl: './reports-list.component.css'
})
export class ReportsListComponent implements  OnInit{

  private reportsService = inject(ReportsService);

  editReport = output<number>();

  reports = input.required<ReportDTO[]>();
  markTypes = signal<MarkType[] | null>(null);

  ngOnInit(): void {    
    this.reportsService.getMarkTypes().subscribe({
      next: (markTypes) => {
        this.markTypes.set(markTypes);    
      }
    })
  }

  getMarkValue(report: ReportDTO, typeId: number): number | null {
    const found = report.marks.find(m => m.markTypeId === typeId);
    return found ? found.markValue : null;
  }

  onReportClick(reportId: number) {
    this.editReport.emit(reportId);
  }
}
