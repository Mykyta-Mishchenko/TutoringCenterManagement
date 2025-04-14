import { Component, inject, input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { UserInfo } from '../../../../shared/models/dto/user-info.dto';
import { ProfileService } from '../../../../shared/services/profile.service';
import { catchError, map, Observable, of, shareReplay } from 'rxjs';
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
  profileUrls = new Map<number, Observable<string>>();
  
  getProfileImgUrl(userId: number): Observable<string> {
    if (!this.profileUrls.has(userId)) {
      const url$ = this.profileService.getUserProfile(userId).pipe(
        map(imgUrl => imgUrl),
        catchError(() => of('empty-profile.png')),
        shareReplay(1)
      );
      this.profileUrls.set(userId, url$);
    }
    return this.profileUrls.get(userId)!;
  }
}
