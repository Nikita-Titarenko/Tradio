import { Routes } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';
import { GuestGuard } from './core/guards/guest.guard';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./layouts/main/main-layout.component').then(
        (m) => m.MainLayoutComponent,
      ),
    children: [
      {
        path: '',
        loadComponent: () =>
          import('./features/home/home.component').then((m) => m.HomeComponent),
        title: 'Tradio',
      },
      {
        path: 'services',
        loadComponent: () =>
          import('./features/services/services/services.component').then(
            (m) => m.ServicesComponent,
          ),
        title: 'Tradio',
        canActivate: [AuthGuard],
      },
      {
        path: 'service',
        loadComponent: () =>
          import('./features/services/service/service.component').then(
            (m) => m.ServiceComponent,
          ),
        title: 'Tradio',
        canActivate: [AuthGuard],
      },
      {
        path: 'chats',
        loadComponent: () =>
          import('./features/chats/chats.component').then(
            (m) => m.ChatsComponent,
          ),
        title: 'Tradio',
        canActivate: [AuthGuard],
      },
      {
        path: 'my-services',
        loadComponent: () =>
          import('./features/services/my-services/my-services.component').then(
            (m) => m.MyServicesComponent,
          ),
        title: 'Tradio',
        canActivate: [AuthGuard],
      },
      {
        path: 'edit-service',
        loadComponent: () =>
          import('./features/services/edit-service/edit-service.component').then(
            (m) => m.EditServiceComponent,
          ),
        title: 'Tradio',
        canActivate: [AuthGuard],
      },
      {
        path: 'create-service',
        loadComponent: () =>
          import('./features/services/create-service/create-service.component').then(
            (m) => m.CreateServiceComponent,
          ),
        title: 'Tradio',
        canActivate: [AuthGuard],
      },
      {
        path: 'climates',
        loadComponent: () =>
          import('./features/climates/climates.component').then(
            (m) => m.ClimatesComponent,
          ),
        title: 'Tradio',
        canActivate: [AuthGuard],
      },
      {
        path: 'payments',
        loadComponent: () =>
          import('./features/payments/payments.component').then(
            (m) => m.PaymentsComponent,
          ),
        title: 'Tradio',
        canActivate: [AuthGuard],
      },
      {
        path: 'users',
        loadComponent: () =>
          import('./features/users/users.component').then(
            (m) => m.UsersComponent,
          ),
        title: 'Tradio',
        canActivate: [AuthGuard],
      },
      {
        path: 'database',
        loadComponent: () =>
          import('./features/database/database.component').then(
            (m) => m.DatabaseComponent,
          ),
        title: 'Tradio',
        canActivate: [AuthGuard],
      },
    ],
  },
  {
    path: '',
    loadComponent: () =>
      import('./layouts/auth/auth-layout.component').then(
        (m) => m.AuthLayoutComponent,
      ),
    children: [
      {
        path: 'register',
        loadComponent: () =>
          import('./features/auth/register/register.component').then(
            (m) => m.RegisterComponent,
          ),
        title: 'Register',
        canActivate: [GuestGuard],
      },
      {
        path: 'login',
        loadComponent: () =>
          import('./features/auth/login/login.component').then(
            (m) => m.LoginComponent,
          ),
        title: 'Login',
        canActivate: [GuestGuard],
      },
      {
        path: 'confirm-email',
        loadComponent: () =>
          import('./features/auth/confirm-email/confirm-email.component').then(
            (m) => m.ConfirmEmailComponent,
          ),
        title: 'Confirm email',
        canActivate: [GuestGuard],
      },
    ],
  },
];
