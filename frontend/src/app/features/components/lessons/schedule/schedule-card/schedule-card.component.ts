import { Component, computed, input, output } from '@angular/core';
import { DatePipe } from '@angular/common';
import { Lesson } from '../../../../../shared/models/lesson.model';
import { Roles } from '../../../../../shared/models/roles.enum';

@Component({
  selector: 'app-schedule-card',
  standalone: true,
  imports: [DatePipe],
  templateUrl: './schedule-card.component.html',
  styleUrl: './schedule-card.component.css'
})
export class ScheduleCardComponent{
  lesson = input.required<Lesson>();
  isOnOwnPage = input.required<boolean>();
  isOnTeacherPage = input.required<boolean>();
  currentUserRole = input.required<Roles>();

  isTeacher = computed(() => this.currentUserRole() === Roles.Teacher);
  isCardActive = computed(() => {
    return (
      !this.isLessonFull() && this.isOnTeacherPage() && this.currentUserRole() === Roles.Student) 
      || this.isOnOwnPage();
  });

  selectedLesson = output<Lesson>();

  isLessonFull = computed(() => this.lesson().studentsCount === this.lesson().lessonType.maxStudentsCount);
  lessonPrice = computed(() => {
    let lessonPrice = this.lesson().lessonType.price / this.lesson().lessonType.maxStudentsCount;
    if (this.isTeacher()) {
      lessonPrice *= this.lesson().studentsCount;
    }
    const roundedPrice = Math.floor(lessonPrice / 5) * 5;
    return roundedPrice;
  });

  onClick() {
    if (this.isCardActive()) {
      this.selectedLesson.emit(this.lesson());    
    }
  }
}