import { Component, computed, input } from '@angular/core';
import { Teacher } from '../../../../shared/models/teacher.model';
import { Subject } from '../../../../shared/models/subject.model';


@Component({
  selector: 'app-teacher-card',
  standalone: true,
  imports: [],
  templateUrl: './teacher-card.component.html',
  styleUrl: './teacher-card.component.css'
})
export class TeacherCardComponent {
  teacher = input.required<Teacher>();

  readonly uniqueSubjects = computed(() => {
    const subjects = this.teacher().subjects;
    return Array.from(
      new Map(subjects.map(subject => [subject.name, subject])).values()
    );
  });

  getSubjectMinMaxYear(targetSubject: Subject) {
    const minYear = this.teacher().subjects
      .filter(subject => subject.name === targetSubject.name)
      .reduce((min, current) => 
        current.classYear < min ? current.classYear : min, 
        Infinity
    );
    
    const maxYear = this.teacher().subjects
    .filter(subject => subject.name === targetSubject.name)
    .reduce((max, current) => 
      current.classYear > max ? current.classYear : max, 
      0
    );

    let minMaxYearString = "";

    if (minYear === maxYear) {
      minMaxYearString = minYear.toString();
    }
    else {
      minMaxYearString = minYear + " - " + maxYear;
    }

    return minMaxYearString;
  }
}
