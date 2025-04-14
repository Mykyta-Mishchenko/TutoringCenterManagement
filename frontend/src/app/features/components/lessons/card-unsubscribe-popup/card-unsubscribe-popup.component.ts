import { Component, computed, inject, input, output } from '@angular/core';
import { BoardService } from '../../../services/board.service';
import { LessonService } from '../../../services/lesson.service';
import { DatePipe, NgIf } from '@angular/common';
import { Lesson } from '../../../../shared/models/lesson.model';
import { AuthService } from '../../../../core/services/auth/auth.service';

@Component({
  selector: 'app-card-unsubscribe-popup',
  standalone: true,
  imports: [NgIf, DatePipe],
  templateUrl: './card-unsubscribe-popup.component.html',
  styleUrl: './card-unsubscribe-popup.component.css'
})
export class CardUnsubscribePopupComponent {
  private authService = inject(AuthService);
  private lessonService = inject(LessonService);
  private boardService = inject(BoardService);

  show = input.required<boolean>();
  lesson = input.required<Lesson | null>();
  close = output();

  lessonPrice = computed(() => {
      const lessonPrice = this.lesson()!.lessonType.price / this.lesson()!.lessonType.maxStudentsCount; 
      const roundedPrice = Math.floor(lessonPrice / 5) * 5;
      return roundedPrice;
    });

  closeModal() {
    this.close.emit();
  }

  unsubscribe() {
    if (this.lesson()?.lessonId) {
      this.lessonService.unsubscribeLesson(this.lesson()!.lessonId).subscribe({
        next: () => {
          this.boardService.loadUserLessons(this.authService.User()!.userId).subscribe();
          this.close.emit();
        }
      });
    }
  }

}
