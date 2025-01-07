import {Component, inject} from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {RouterLink} from '@angular/router';
import {AuthService} from '../../auth/auth.service';

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
  authService: AuthService = inject(AuthService);

  form = new FormGroup({
    email: new FormControl<string | null>(null, [Validators.required, Validators.email]),
    password: new FormControl<string | null>(null, [Validators.required, Validators.minLength(6)])
  })

  onLogin(): void {
    if (this.form.valid) {
      const loginData = {
        email: this.form.get('email')?.value as string,
        password: this.form.get('password')?.value as string
      }

      this.authService.login(loginData).subscribe({
        next: (response) => console.log('Login Successful:', response),
        error: (err) => console.error('Login Failed:', err)
      })
    } else {
      console.log('Login Failed: Invalid Form');
    }
  }
}
