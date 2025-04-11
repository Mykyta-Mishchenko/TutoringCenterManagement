import { Component, inject, OnInit, signal } from '@angular/core';
import { ScheduleBoardComponent } from "../../schedule/schedule-board/schedule-board.component";
import { ProfileComponent } from "../profile/profile.component";
import { AuthService } from '../../../../../core/services/auth/auth.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [ScheduleBoardComponent, ProfileComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  
  private route = inject(ActivatedRoute);
  private authService = inject(AuthService);

  selectedUserId = signal<number>(this.authService.User()!.userId);

  isOnOwnPage = signal<boolean>(false);

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      const userId = params['userId'];
      if (userId) {
        this.updateUserInfo();
      }
    });
  }

  updateUserInfo() {
    this.selectedUserId.set(Number(this.route.snapshot.queryParamMap.get('userId')));
    if (this.selectedUserId() == this.authService.User()?.userId) {
      this.isOnOwnPage.set(true);
    }
    else {
      this.isOnOwnPage.set(false);
    }
  }
}
