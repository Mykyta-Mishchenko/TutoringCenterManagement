import { Component, inject, OnInit, output, signal, Signal } from '@angular/core';
import { Subject } from '../../../../shared/models/subject.model';
import { form as reactiveForm } from './search.reactive-form';
import { LessonService } from '../../../services/lesson.service';
import { ReactiveFormsModule } from '@angular/forms';
import { UsersFilter } from '../../../../shared/models/dto/users-filter.dto';
import { Roles } from '../../../../shared/models/roles.enum';

@Component({
  selector: 'app-search-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './search-form.component.html',
  styleUrl: './search-form.component.css'
})
export class SearchFormComponent implements OnInit {
  
  private lessonService = inject(LessonService);

  filter = output<UsersFilter>();

  form = reactiveForm;
  subjects = signal<Subject[] | null>(null);

  ngOnInit(): void {
    this.lessonService.getSubjects().subscribe({
      next: (subjects) => {
        this.subjects.set(subjects);
      }
    })
  }

  getRange(start: number, end: number): number[] {
    return Array(end - start + 1).fill(0).map((_, idx) => start + idx);
  }

  onSubmit() {
    const filter: UsersFilter = {
      role: Roles.Teacher,
      name: this.form.controls.name.value,
      subjectId: this.form.controls.subjectId.value,
      schoolYear: this.form.controls.schoolYear.value,
      page: 1,
      perPage: 20
    }
    this.filter.emit(filter);
  }
}
