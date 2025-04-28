import { Component, effect, inject, input,OnChanges,OnInit, signal } from '@angular/core';
import { ProfileInfoComponent } from "../profile-info/profile-info.component";
import { HasRoleDirective } from '../../../../../core/directives/role.directive';
import { ModalState } from '../../models/modal-state.enum';
import { CardEditingPopupComponent } from "../../card-editing-popup/card-editing-popup.component";
import { UsersService } from '../../../../services/users.service';
import { ProfileImgComponent } from "../profile-img/profile-img.component";
import { UserInfo } from '../../../../../shared/models/dto/user-info.dto';
import { BoardService } from '../../../../services/board.service';
import { ExternalApiService } from '../../../../services/external-api.service';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [ProfileInfoComponent, HasRoleDirective, CardEditingPopupComponent, ProfileImgComponent],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnChanges {
  private usersService = inject(UsersService);
  private boardService = inject(BoardService);
  private externalApiService = inject(ExternalApiService);

  selectedUserId = input.required<number>()
  selectedUser = signal<UserInfo | null>(null);

  isOnOwnPage = input.required<boolean>();

  isModalVisible = signal<boolean>(false);
  modalState = signal<ModalState>(ModalState.Creating);

  constructor() {
      effect(() => {
        const lessons = this.boardService.lessons();
        this.getUserInfo();
      });
    }
  
  ngOnChanges(): void {
    this.getUserInfo();
  }

  getUserInfo() {
    this.usersService.getUser(this.selectedUserId()).subscribe({
      next: (user) => {
        this.selectedUser.set(user);
      }
    });
  }

  onAddNewLesson() {
    this.isModalVisible.set(true);
  }

  onDownloadSchedule() {
    this.externalApiService.getTeacherSchedule(this.selectedUserId()).subscribe({
      next: (schedule) => {

        const blob = new Blob([JSON.stringify(schedule, null, 2)], { type: 'text/plain;charset=utf-8' });
        const url = window.URL.createObjectURL(blob);

        const link = document.createElement('a');
        link.href = url;
        link.download = 'schedule.json';
        link.click();

        window.URL.revokeObjectURL(url);
      }
    })
  }

  onUpdate() {
    this.isModalVisible.set(false);
    this.getUserInfo();
  }

  onModalClose() {
    this.isModalVisible.set(false);
  }
}
