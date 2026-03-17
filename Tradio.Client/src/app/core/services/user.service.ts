import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, ReplaySubject, tap } from 'rxjs';
import { RegisterResponseModel } from '../responses/register-response.model';
import { SignInResponseModel } from '../responses/sign-in-response.model';
import { UserModel } from '../responses/user.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
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

  private userSource = new ReplaySubject<UserModel>(1);
  public user$ = this.userSource.asObservable();

  getUser(): Observable<UserModel> {
    return this.http
      .get<UserModel>(`${this.apiUrl}/${this.userId}`)
      .pipe(tap((user) => this.userSource.next(user)));
  }

  get isLoggedIn() {
    return !!localStorage.getItem(this.jwtTokenName);
  }

  get userId() {
    const token = localStorage.getItem(this.jwtTokenName);
    if (!token) {
      return null;
    }
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload.nameid;
    } catch {
      return null;
    }
  }

  saveJwtToken(jwtToken: string) {
    localStorage.setItem(this.jwtTokenName, jwtToken);
  }

  logout() {
    localStorage.removeItem(this.jwtTokenName);
  }
}
