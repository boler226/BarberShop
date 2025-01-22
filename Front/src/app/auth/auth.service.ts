import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {TokenResponse} from './auth.interface';
import {catchError, of, tap, throwError} from 'rxjs';
import {CookieService} from 'ngx-cookie-service';
import {Router} from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  http: HttpClient = inject(HttpClient)
  cookieService = inject(CookieService)
  router = inject(Router)
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
       this.saveTokens(val)
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

  refresh() {
    return this.http.post<TokenResponse>(
      `${this.baseUrl}Refresh`, {
        refreshToken: this.refreshToken
      })
      .pipe(
        tap(val => {
          this.saveTokens(val)
        }),

        catchError(error => {
          this.logout()
          return throwError(error)
        })
      )
  }

  logout() {
    this.cookieService.deleteAll()
    this.accessToken = null
    this.refreshToken = null
    this.router.navigate(['/login'])
  }

  saveTokens(res: TokenResponse) {
    this.accessToken = res.access_token
    this.refreshToken = res.refresh_token

    this.cookieService.set('accessToken', this.accessToken)
    this.cookieService.set('refreshToken', this.refreshToken)
  }

  get isAuth() {
    if (!this.accessToken) {
      this.accessToken = this.cookieService.get('accessToken')
      this.refreshToken = this.cookieService.get('refreshToken')
    }

    return !!this.accessToken
  }
}
