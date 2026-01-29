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
      }
    ]
  }
];
