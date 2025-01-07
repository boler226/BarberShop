import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  http: HttpClient = inject(HttpClient);
  baseUrl: string = 'https://localhost:7142/api/Accounts/';

  login(payload: {email: string, password: string}) {
    const formData = new FormData();
    formData.append('Email', payload.email);
    formData.append('Password', payload.password);

    return this.http.post(`${this.baseUrl}SignIn`, formData);
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
}
