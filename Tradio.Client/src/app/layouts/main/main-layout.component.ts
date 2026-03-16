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
  public creditsCount$?: Observable<number>;
  constructor(
    public authService: UserService,
    public router: Router,
  ) {}
  ngOnInit(): void {
    this.creditsCount$ = this.authService
      .getUser()
      .pipe(map((user) => user.creditCount));
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
