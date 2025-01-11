import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {TokenResponse} from './auth.interface';
import {tap} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  http: HttpClient = inject(HttpClient)
  baseUrl: string = 'https://localhost:7142/api/Accounts/'

  accessToken: string | null = null
  refreshToken: string | null = null

  constructor() {
    this.accessToken = localStorage.getItem('accessToken') || null;
    this.refreshToken = localStorage.getItem('refreshToken') || null;
  }

  login(payload: {email: string, password: string}) {
    const formData = new FormData();
    formData.append('Email', payload.email);
    formData.append('Password', payload.password);

    return this.http.post<TokenResponse>(
      `${this.baseUrl}SignIn`,
      formData
    ).pipe(
      tap(val => {
        this.accessToken = val.access_token
        this.refreshToken = val.refresh_token

        localStorage.setItem('accessToken', val.access_token);
        localStorage.setItem('refreshToken', val.refresh_token);
      })
    )
  }

  register(payload: {firstName: string, lastName: string, email: string, userName: string, password: string, image: File}) {
    const formData = new FormData();
    formData.append('FirstName', payload.firstName);
    formData.append('LastName', payload.lastName);
    formData.append('Email', payload.email);
    formData.append('UserName', payload.userName);
    formData.append('Password', payload.password);
    formData.append('Image', payload.image);

    return this.http.post(`${this.baseUrl}Registration`, formData);
  }

  get isAuth() {
    return !!this.accessToken || !!localStorage.getItem('accessToken');
  }
}
