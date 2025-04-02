import { Component } from '@angular/core';
import { AuthHeaderComponent } from "../auth-header/auth-header.component";

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [AuthHeaderComponent],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {

}
