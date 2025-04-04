import { Component, computed, inject } from '@angular/core';
import { AuthService } from '../../../core/services/auth/auth.service';
import { NgClass, NgIf } from '@angular/common';
import { ProfileService } from '../../../shared/services/profile.service';
import { ImgProfileService } from '../../services/img-profile.service';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [NgClass, NgIf],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {
  private authService = inject(AuthService);
  private imgProfileService = inject(ImgProfileService);

  user = this.authService.User;
  profileImgUrl = computed(() => this.user()?.profileImgUrl ?? 'empty-profile.png');

  selectedFile: File | null = null;
  errorMessage: string | null = null;

  triggerFileInput() {
    const fileInput = document.querySelector<HTMLInputElement>('#fileInput');
    if (fileInput) fileInput.click();
  }


  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    this.errorMessage = null;

    if (input.files && input.files.length > 0) {
      const file = input.files[0];
      
      const validationResult = this.imgProfileService.validateFile(file);
      if (!validationResult.isValid) {
        this.errorMessage = validationResult.errorMessage || null;
        return;
      }
  
      this.selectedFile = file;
      this.imgProfileService.uploadFile(this.selectedFile);
    }
  }
}
