import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'main-layout',
  templateUrl: './main-layout.component.html',
  host: {
    class: 'flex-column'
  },
  imports: [
    CommonModule,
    RouterOutlet,
    RouterLink,
    RouterLinkActive
  ]
})
export class MainLayoutComponent {
  constructor(public authService: AuthService, public router: Router){}

  logout(){
    this.authService.logout();
  }
}
