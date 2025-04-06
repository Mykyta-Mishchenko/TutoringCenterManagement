import { Component, input } from '@angular/core';
import { Lesson } from '../../../shared/models/lesson.model';

@Component({
  selector: 'app-schedule-card',
  standalone: true,
  imports: [],
  templateUrl: './schedule-card.component.html',
  styleUrl: './schedule-card.component.css'
})
export class ScheduleCardComponent {
  lesson = input.required<Lesson>();
}
