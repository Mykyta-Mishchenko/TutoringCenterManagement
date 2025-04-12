import { Component, inject, input, OnInit, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { UserInfo } from '../../../../shared/models/dto/user-info.dto';
import { ProfileService } from '../../../../shared/services/profile.service';


@Component({
  selector: 'app-teacher-card',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './teacher-card.component.html',
  styleUrl: './teacher-card.component.css'
})
export class TeacherCardComponent implements OnInit{

  private profileService = inject(ProfileService);

  teacher = input.required<UserInfo>();
  profileImgUrl = signal<string>('empty-profile.png');

  ngOnInit(): void {
    this.getProfileImgUrl();
  }

  getProfileImgUrl() {
    this.profileService.getUserProfile(this.teacher().userId).subscribe(
      {
        next: (imgUrl) => {
          this.profileImgUrl.set(imgUrl)
        },
        error: (err) => {
          this.profileImgUrl.set('empty-profile.png');
        }
      }
    )
  }
}
