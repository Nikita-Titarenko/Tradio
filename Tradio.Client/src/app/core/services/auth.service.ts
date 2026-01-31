import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RegisterResponseModel } from '../responses/register-response.model';
import { SignInResponseModel } from '../responses/sign-in-response.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'http://localhost:5188/api/Users';
  private jwtTokenName = 'jwtToken';

  constructor(private http: HttpClient) {}

  login(email: string, password: string): Observable<SignInResponseModel> {
    return this.http.post<SignInResponseModel>(`${this.apiUrl}/login`, {
      email,
      password,
    });
  }

  register(
    name: string,
    email: string,
    password: string,
    confirmPassword: string,
    cityId: number,
  ): Observable<RegisterResponseModel> {
    return this.http.post<RegisterResponseModel>(`${this.apiUrl}/register`, {
      name,
      email,
      password,
      confirmPassword,
      cityId,
    });
  }

  confirmEmail(code: string, userId: string): Observable<SignInResponseModel> {
    return this.http.post<SignInResponseModel>(`${this.apiUrl}/confirm-email`, {
      code,
      userId,
    });
  }

  get isLoggedIn() {
    return !!localStorage.getItem(this.jwtTokenName);
  }

  saveJwtToken(jwtToken: string) {
    localStorage.setItem(this.jwtTokenName, jwtToken);
  }

  logout() {
    localStorage.removeItem(this.jwtTokenName);
  }
}
