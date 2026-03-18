import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { UserService } from '../core/services/user.service';
import { UserModel } from '../core/responses/user.model';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css'],
  imports: [CommonModule, ReactiveFormsModule],
  host: { class: 'd-flex w-100' },
})
export class UsersComponent {
  users$!: Observable<UserModel[]>;
  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.users$ = this.userService.getUsers();
  }

  banUser(userId: string): void {
    this.userService.banUser({ userId }).subscribe(() => {
      this.users$ = this.userService.getUsers();
    });
  }

  unbanUser(userId: string): void {
    this.userService.unbanUser({ userId }).subscribe(() => {
      this.users$ = this.userService.getUsers();
    });
  }
}
