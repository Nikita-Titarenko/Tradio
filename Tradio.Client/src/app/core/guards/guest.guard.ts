import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { UserService } from '../services/user.service';

@Injectable({ providedIn: 'root' })
export class GuestGuard implements CanActivate {
  constructor(
    private authService: UserService,
    private router: Router,
  ) {}

  canActivate(): boolean {
    if (this.authService.isLoggedIn) {
      this.router.navigate(['/services']);
      return false;
    }

    return true;
  }
}
