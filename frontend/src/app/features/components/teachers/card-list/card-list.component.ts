import { Component, input } from '@angular/core';
import { TeacherCardComponent } from "../teacher-card/teacher-card.component";
import { Teacher } from '../../../../shared/models/teacher.model';

@Component({
  selector: 'app-card-list',
  standalone: true,
  imports: [TeacherCardComponent],
  templateUrl: './card-list.component.html',
  styleUrl: './card-list.component.css'
})
export class CardListComponent {
  teachers = input.required<Teacher[]>()
}
