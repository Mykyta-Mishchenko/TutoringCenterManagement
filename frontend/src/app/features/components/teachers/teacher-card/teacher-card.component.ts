import { Component, computed, inject, input } from '@angular/core';
import { Teacher } from '../../../../shared/models/teacher.model';
import { Subject } from '../../../../shared/models/subject.model';
import { SubjectInfoService } from '../../../services/subject-info.service.ts.service';
import { RouterLink } from '@angular/router';


@Component({
  selector: 'app-teacher-card',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './teacher-card.component.html',
  styleUrl: './teacher-card.component.css'
})
export class TeacherCardComponent {

  private subjectInfoService = inject(SubjectInfoService);
  teacher = input.required<Teacher>();

  getUniqueSubjects(): Subject[] {
    return this.subjectInfoService.getUniqueSubjects(this.teacher().lessons);
  }

  getSubjectMinMaxYear(subject: Subject) {
    return this.subjectInfoService.getSubjectMinMaxYear(this.teacher().lessons, subject);
  }
}
