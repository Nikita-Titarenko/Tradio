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
          import('./home/home.component').then((m) => m.HomeComponent),
        title: 'Tradio',
      },
      {
        path: 'services',
        loadComponent: () =>
          import('./services/services/services.component').then(
            (m) => m.ServicesComponent,
          ),
        title: 'Tradio',
        canActivate: [AuthGuard],
      },
      {
        path: 'service',
        loadComponent: () =>
          import('./services/service/service.component').then(
            (m) => m.ServiceComponent,
          ),
        title: 'Tradio',
        canActivate: [AuthGuard],
      },
      {
        path: 'chats',
        loadComponent: () =>
          import('./chats/chats.component').then((m) => m.ChatsComponent),
        title: 'Tradio',
        canActivate: [AuthGuard],
      },
      {
        path: 'my-services',
        loadComponent: () =>
          import('./services/my-services/my-services.component').then(
            (m) => m.MyServicesComponent,
          ),
        title: 'Tradio',
        canActivate: [AuthGuard],
      },
      {
        path: 'create-service',
        loadComponent: () =>
          import('./services/create-service/create-service.component').then(
            (m) => m.CreateServiceComponent,
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
          import('./auth/register/register.component').then(
            (m) => m.RegisterComponent,
          ),
        title: 'Register',
        canActivate: [GuestGuard],
      },
      {
        path: 'login',
        loadComponent: () =>
          import('./auth/login/login.component').then((m) => m.LoginComponent),
        title: 'Login',
        canActivate: [GuestGuard],
      },
      {
        path: 'confirm-email',
        loadComponent: () =>
          import('./auth/confirm-email/confirm-email.component').then(
            (m) => m.ConfirmEmailComponent,
          ),
        title: 'Confirm email',
        canActivate: [GuestGuard],
      },
    ],
  },
];
