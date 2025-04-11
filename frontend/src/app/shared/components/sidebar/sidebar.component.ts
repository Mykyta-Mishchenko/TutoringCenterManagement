import { Component, computed, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth/auth.service';
import { Roles } from '../../models/roles.enum';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
  private authService = inject(AuthService);

  userId = this.authService.User()?.userId;

  userRole = computed(() => {
    const role = this.authService.User()!.role.toString();
    return String(role[0]).toUpperCase() + String(role).slice(1);
  });
}
