import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'main-layout',
  templateUrl: './main-layout.component.html',
  imports: [
    CommonModule,
    RouterOutlet,
    RouterLink
  ]
})
export class MainLayoutComponent {
  constructor(public authService: AuthService){}

  logout(){
    this.authService.logout();
  }
}
