import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
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
import { TranslateModule, TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'main-layout',
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.css'],
  host: {
    class: 'flex-column',
  },
  imports: [
    CommonModule,
    RouterOutlet,
    RouterLink,
    RouterLinkActive,
    TranslateModule,
  ],
})
export class MainLayoutComponent implements OnInit {
  private translate = inject(TranslateService);
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

  changeLang(lang: string) {
    this.translate.use(lang);
  }

  isCurrentLang(lang: string): boolean {
    return this.translate.currentLang === lang;
  }
}
