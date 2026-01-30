import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./layouts/main/main-layout.component').then(m => m.MainLayoutComponent),
    children: [
      {
        path: '',
        loadComponent: () => import('./home.component').then(m => m.HomeComponent),
        title: 'Tradio'
      },
      {
        path: 'services',
        loadComponent: () => import('./services/services/services.component').then(m => m.ServicesComponent),
        title: 'Tradio'
      }
    ]
  },
  {
    path: '',
    loadComponent: () => import('./layouts/auth/auth-layout.component').then(m => m.AuthLayoutComponent),
    children: [
      {
        path: 'register',
        loadComponent: () => import('./auth/register/register.component').then(m => m.RegisterComponent),
        title: "Register"
      },
      {
        path: 'login',
        loadComponent: () => import('./auth/login/login.component').then(m => m.LoginComponent),
        title: "Login"
      },
      {
        path: 'confirm-email',
        loadComponent: () => import('./auth/confirm-email/confirm-email.component').then(m => m.ConfirmEmailComponent),
        title: "Confirm email"
      }
    ]
  }
];
