import { Component, computed, input } from '@angular/core';
import { Teacher } from '../../../../shared/models/teacher.model';
import { Subject } from '../../../../shared/models/subject.model';

@Component({
  selector: 'app-table-list',
  standalone: true,
  imports: [],
  templateUrl: './table-list.component.html',
  styleUrl: './table-list.component.css'
})
export class TableListComponent {
  teachers = input.required<Teacher[]>()

  getUniqueSubjects(teacher: Teacher) {
    const subjects = teacher.subjects;
    return Array.from(
      new Map(subjects.map(subject => [subject.name, subject])).values()
    );
  }
  
    getSubjectMinMaxYear(teacher: Teacher, targetSubject: Subject) {
      const minYear = teacher.subjects
        .filter(subject => subject.name === targetSubject.name)
        .reduce((min, current) => 
          current.classYear < min ? current.classYear : min, 
          Infinity
      );
      
      const maxYear = teacher.subjects
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
