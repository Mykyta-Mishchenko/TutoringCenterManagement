import { Component, inject, input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { UserInfo } from '../../../../shared/models/dto/user-info.dto';
import { ProfileService } from '../../../../shared/services/profile.service';
import { catchError, Observable, of } from 'rxjs';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-table-list',
  standalone: true,
  imports: [RouterLink, AsyncPipe],
  templateUrl: './table-list.component.html',
  styleUrl: './table-list.component.css'
})
export class TableListComponent {

  private profileService = inject(ProfileService);

  teachers = input.required<UserInfo[] | null>()
  
  getProfileImgUrl(userId: number): Observable<string> {
    return this.profileService.getUserProfile(userId).pipe(
      catchError(() => of('empty-profile.png'))
    );
  }
}
