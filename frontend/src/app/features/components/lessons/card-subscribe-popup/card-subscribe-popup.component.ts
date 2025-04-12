import { Component, computed, inject, input, output } from '@angular/core';
import { LessonService } from '../../../services/lesson.service';
import { DatePipe, NgIf } from '@angular/common';
import { Lesson } from '../../../../shared/models/lesson.model';

@Component({
  selector: 'app-card-subscribe-popup',
  standalone: true,
  imports: [NgIf, DatePipe],
  templateUrl: './card-subscribe-popup.component.html',
  styleUrl: './card-subscribe-popup.component.css'
})
export class CardSubscribePopupComponent {
  private lessonService = inject(LessonService);
  
  show = input.required<boolean>();
  lesson = input<Lesson | null>();
  close = output();
  
  lessonPrice = computed(() => {
    const lessonPrice = this.lesson()!.lessonType.price / this.lesson()!.lessonType.maxStudentsCount; 
    const roundedPrice = Math.floor(lessonPrice / 5) * 5;
    return roundedPrice;
  });
  
  closeModal() {
    this.close.emit();
  }
  
  subscribe() {
    if (this.lesson()?.lessonId) {
      this.lessonService.subscribeLesson(this.lesson()!.lessonId);
      this.close.emit();
    }
  }
}
