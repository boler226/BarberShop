import {Component, inject} from '@angular/core';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {AuthService} from '../../auth/auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-register-page',
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './register-page.component.html',
  standalone: true,
  styleUrl: './register-page.component.scss'
})
export class RegisterPageComponent {
  authService: AuthService = inject(AuthService)
  private selectedFile: File | null = null
  private router = inject(Router)

  form = new FormGroup({
    firstName: new FormControl(null, [Validators.required]),
    lastName: new FormControl(null, [Validators.required]),
    email: new FormControl(null, [Validators.required, Validators.email]),
    userName: new FormControl(null, [Validators.required]),
    password: new FormControl(null, [Validators.required, Validators.minLength(6)])
  })

  onFileChange(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      //@ts-ignore
      this.selectedFile = input.files[0]
    }
  }

  onRegister(): void {
    if (this.form.value && this.selectedFile) {
      const registerData = {
        firstName: this.form.get('firstName')?.value ?? '',
        lastName: this.form.get('lastName')?.value ?? '',
        email: this.form.get('email')?.value ?? '',
        userName: this.form.get('userName')?.value ?? '',
        password: this.form.get('password')?.value ?? '',
        image: this.selectedFile as File
      }
      this.authService.register(registerData)
        .subscribe({
          next: (response) => {
            console.log('Registration Successful:', response)
            this.router.navigate(['/login']);
          },
          error: (err) => console.error('Registration Failed:', err)
      })
    } else {
      console.log('Registration Failed: Invalid Form');
    }
  }
}
