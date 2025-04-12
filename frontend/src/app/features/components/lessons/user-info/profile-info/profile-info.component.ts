import { Component, input,} from '@angular/core';
import { SubjectInfo } from '../../../../../shared/models/dto/subject-info.dto';

@Component({
  selector: 'app-profile-info',
  standalone: true,
  imports: [],
  templateUrl: './profile-info.component.html',
  styleUrl: './profile-info.component.css'
})
export class ProfileInfoComponent{
  subjects = input.required<SubjectInfo[] | undefined>();
}
