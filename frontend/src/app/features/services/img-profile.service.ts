import { inject, Injectable } from "@angular/core";
import { ProfileService } from "../../shared/services/profile.service";
import { AuthService } from "../../core/services/auth/auth.service";
import { Observable, throwError } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class ImgProfileService{

    private authService = inject(AuthService);
    private profileService = inject(ProfileService);

    validateFile(file: File): { isValid: boolean, errorMessage?: string } {
        if (!file) {
          return { isValid: false, errorMessage: "File is required." };
        }
      
        if (file.size <= 0) {
          return { isValid: false, errorMessage: "File cannot be empty." };
        }
      
        const maxSize = 2 * 1024 * 1024; // 2MB
        if (file.size > maxSize) {
          return { isValid: false, errorMessage: "Maximum file size is 2MB." };
        }
      
        const allowedExtensions = ['.jpg', '.png', '.jpeg'];
        const extension = file.name.toLowerCase().substring(file.name.lastIndexOf('.'));
        if (!allowedExtensions.includes(extension)) {
          return { isValid: false, errorMessage: "Only image files (.jpg, .jpeg, .png) are allowed." };
        }
      
        return { isValid: true };
      }
    
      uploadFile(selectedFile: File | null): Observable<any> {
        if (!selectedFile) return throwError(() => new Error('No file selected.'));;
      
        const formData = new FormData();
        formData.append('profileImg', selectedFile);
      
        return this.profileService.setUserProfile(formData);
      }
}