import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  Router,
  RouterLink,
  RouterLinkActive,
  RouterOutlet,
} from '@angular/router';
import { UserService } from '../../core/services/user.service';
import { map } from 'rxjs/internal/operators/map';
import { Observable } from 'rxjs';
import { UserModel } from '../../core/responses/user.model';

@Component({
  selector: 'main-layout',
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.css'],
  host: {
    class: 'flex-column',
  },
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive],
})
export class MainLayoutComponent implements OnInit {
  public userModel$?: Observable<UserModel>;

  constructor(
    public authService: UserService,
    public router: Router,
  ) {}
  ngOnInit(): void {
    this.userModel$ = this.authService.user$;

    this.authService.getUser().subscribe();
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
