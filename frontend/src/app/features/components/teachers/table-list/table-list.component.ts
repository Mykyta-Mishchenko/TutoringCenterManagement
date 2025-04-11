import { Component, computed, inject, input } from '@angular/core';
import { Teacher } from '../../../../shared/models/teacher.model';
import { Subject } from '../../../../shared/models/subject.model';
import { SubjectInfoService } from '../../../services/subject-info.service.ts.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-table-list',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './table-list.component.html',
  styleUrl: './table-list.component.css'
})
export class TableListComponent {
  private subjectInfoService = inject(SubjectInfoService);
  teachers = input.required<Teacher[]>()

  getUniqueSubjects(teacher: Teacher): Subject[] {
    return this.subjectInfoService.getUniqueSubjects(teacher.lessons);
  }

  getSubjectMinMaxYear(teacher: Teacher, subject: Subject) {
    return this.subjectInfoService.getSubjectMinMaxYear(teacher.lessons, subject);
  }
}
