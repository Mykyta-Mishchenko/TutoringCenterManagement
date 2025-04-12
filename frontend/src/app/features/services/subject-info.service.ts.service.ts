import { Injectable } from '@angular/core';
import { Subject } from '../../shared/models/subject.model';
import { Lesson } from '../../shared/models/lesson.model';

@Injectable({
  providedIn: 'root'
})
export class SubjectInfoService {
  getUniqueSubjects(lessons: Lesson[]) : Subject[]{
    const subjects = lessons.map(lesson => lesson.lessonType.subject);
    return Array.from(
      new Map(subjects.map(subject => [subject.name, subject])).values()
    );
  }
  
  getSubjectMinMaxYear(lessons: Lesson[], targetSubject: Subject) : string {  
    const minYear = lessons
      .filter(lesson => lesson.lessonType.subject.name === targetSubject.name)
      .reduce((min, current) => 
        current.lessonType.schoolYear < min ? current.lessonType.schoolYear : min, 
        Infinity
      );
    
    const maxYear = lessons
      .filter(lesson => lesson.lessonType.subject.name === targetSubject.name)
      .reduce((max, current) => 
        current.lessonType.schoolYear > max ? current.lessonType.schoolYear : max,
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
