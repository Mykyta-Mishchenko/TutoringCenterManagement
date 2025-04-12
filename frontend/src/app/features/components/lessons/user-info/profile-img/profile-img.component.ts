import { Component, computed, inject, input, OnChanges, OnInit, signal } from '@angular/core';
import { ImgProfileService } from '../../../../services/img-profile.service';
import { ProfileService } from '../../../../../shared/services/profile.service';
import { NgClass, NgIf } from '@angular/common';
import { AuthService } from '../../../../../core/services/auth/auth.service';

@Component({
  selector: 'app-profile-img',
  standalone: true,
  imports: [NgClass, NgIf],
  templateUrl: './profile-img.component.html',
  styleUrl: './profile-img.component.css'
})
export class ProfileImgComponent implements OnChanges {

  private authService = inject(AuthService);
  private profileService = inject(ProfileService);
  private imgProfileService = inject(ImgProfileService);

  userId = input.required<number | undefined>();
  isOnOwnPage = input.required<boolean>();

  profileImgUrl = signal<string>('empty-profile.png');
  isProfileImgEmpty = computed(() => {
      return this.profileImgUrl() === 'empty-profile.png'
  })

  selectedFile: File | null = null;
  errorMessage: string | null = null;

  ngOnChanges() {
    this.getProfileImgUrl();
  }

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
      this.imgProfileService.uploadFile(this.selectedFile).subscribe({
        next: (response) => {
          this.authService.setUserProfile();
          this.getProfileImgUrl();
        },
        error: (error) => {
          console.error('Upload failed:', error);
        }
      });
    }
  }

  getProfileImgUrl() {
    if (this.userId()) {
      this.profileService.getUserProfile(this.userId()!).subscribe(
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
}
