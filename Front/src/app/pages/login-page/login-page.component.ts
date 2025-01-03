import { Component } from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterLink
  ],
  styleUrl: './login-page.component.scss'
})
export class LoginPageComponent {
  loginForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password:  ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  onLogin(): void {
    if (this.loginForm.valid) {
      console.log('Login Successful:', this.loginForm.value);
    } else {
      console.log('Login Failed: Invalid Form');
    }
  }
}
