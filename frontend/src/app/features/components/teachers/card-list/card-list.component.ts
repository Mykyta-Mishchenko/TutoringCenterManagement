import { Component, input } from '@angular/core';
import { TeacherCardComponent } from "../teacher-card/teacher-card.component";
import { UserInfo } from '../../../../shared/models/dto/user-info.dto';

@Component({
  selector: 'app-card-list',
  standalone: true,
  imports: [TeacherCardComponent],
  templateUrl: './card-list.component.html',
  styleUrl: './card-list.component.css'
})
export class CardListComponent {
  teachers = input.required<UserInfo[] | null>()
}
