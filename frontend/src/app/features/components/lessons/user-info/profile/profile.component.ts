import { Component, inject, input, OnChanges, OnInit, signal } from '@angular/core';
import { ProfileInfoComponent } from "../profile-info/profile-info.component";
import { HasRoleDirective } from '../../../../../core/directives/role.directive';
import { ModalState } from '../../models/modal-state.enum';
import { CardEditingPopupComponent } from "../../card-editing-popup/card-editing-popup.component";
import { BoardService } from '../../../../services/board.service';
import { UsersService } from '../../../../services/users.service';
import { User } from '../../../../../shared/models/user.models';
import { AuthService } from '../../../../../core/services/auth/auth.service';
import { ProfileImgComponent } from "../profile-img/profile-img.component";

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [ProfileInfoComponent, HasRoleDirective, CardEditingPopupComponent, ProfileImgComponent],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {
  private boardService = inject(BoardService); 
  private authService = inject(AuthService);
  private usersService = inject(UsersService);

  selectedUserId = input.required<number>()
  selectedUser = signal<User | null>(null);

  isOnOwnPage = input.required<boolean>();

  isModalVisible = signal<boolean>(false);
  modalState = signal<ModalState>(ModalState.Creating);
  
  ngOnInit(): void {
    this.selectedUser.set(this.usersService.getUser(this.selectedUserId()));
  }

  onAddNewLesson() {
    this.isModalVisible.set(true);
  }

  onUpdate() {
    this.isModalVisible.set(false);
    this.boardService.loadUserLessons(this.selectedUserId());
  }

  onModalClose() {
    this.isModalVisible.set(false);
  }
}
