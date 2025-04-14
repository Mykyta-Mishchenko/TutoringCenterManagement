import { Component, computed, inject, input, OnChanges, output, signal } from '@angular/core';
import { LessonService } from '../../../services/lesson.service';
import { DatePipe, NgIf } from '@angular/common';
import { Lesson } from '../../../../shared/models/lesson.model';
import { BoardService } from '../../../services/board.service';
import { catchError, map, Observable, of } from 'rxjs';
import { AuthService } from '../../../../core/services/auth/auth.service';

@Component({
  selector: 'app-card-subscribe-popup',
  standalone: true,
  imports: [NgIf, DatePipe],
  templateUrl: './card-subscribe-popup.component.html',
  styleUrl: './card-subscribe-popup.component.css'
})
export class CardSubscribePopupComponent implements OnChanges{
  private authService = inject(AuthService);
  private boardService = inject(BoardService);
  private lessonService = inject(LessonService);
  
  show = input.required<boolean>();
  userId = input.required<number>();
  lesson = input<Lesson | null>();
  close = output();

  isBadRequest = signal<boolean>(false);
  isInConflict = signal<boolean>(false);
  
  lessonPrice = computed(() => {
    const lessonPrice = this.lesson()!.lessonType.price / this.lesson()!.lessonType.maxStudentsCount; 
    const roundedPrice = Math.floor(lessonPrice / 5) * 5;
    return roundedPrice;
  });
  
  ngOnChanges(): void {
    this.lessonTimeHasConflict().subscribe({
      next: (conflict) => {
        this.isInConflict.set(conflict);
      },
      error: () => {
        this.isInConflict.set(true);
      }
    });
  }

  closeModal() {
    this.close.emit();
  }
  
  subscribe() {
    console.log(this.lesson());
    if (this.isInConflict()) {
      this.close.emit();
    }
    else {
      if (this.lesson()?.lessonId) {
        this.lessonService.subscribeLesson(this.lesson()!.lessonId).subscribe({
          next: () => {
            this.boardService.loadUserLessons(this.userId()).subscribe();
            this.close.emit();
            this.isBadRequest.set(false);
          },
          error: () => {
            this.isBadRequest.set(true);
          }
        });
      }
    }
  }

  lessonTimeHasConflict(): Observable<boolean> {
    const day = Number(this.lesson()?.schedule.dayOfWeek);
    const hour = Number(this.lesson()?.schedule.dayTime.getHours());
    const minutes = Number(this.lesson()?.schedule.dayTime.getMinutes());
  
    if (day == null || hour == null || minutes == null) {
      return of(true);
    }
  
    const newStart = hour * 60 + minutes;
    const newEnd = newStart + 60;

    return this.lessonService.getUserSchedule(this.authService.User()!.userId).pipe(
      map((lessons: Lesson[]) => {
        return lessons.some(lesson =>
        {
          const lessonDay = Number(lesson.schedule.dayOfWeek);
          const lessonStart = lesson.schedule.dayTime.getHours() * 60 +
                              lesson.schedule.dayTime.getMinutes();
          const lessonEnd = lessonStart + 60;
  
          return lessonDay === day &&
                ((newStart >= lessonStart && newStart < lessonEnd) ||
                 (newEnd > lessonStart && newEnd <= lessonEnd));
        });
      }),
      catchError(() => of(true))
    );
  }
}
