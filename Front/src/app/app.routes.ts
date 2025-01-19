import { Routes } from '@angular/router';
import {LoginPageComponent} from './pages/login-page/login-page.component';
import {RegisterPageComponent} from './pages/register-page/register-page.component';
import {LayoutComponent} from './common-ui/layout/layout.component';
import {canActivateAuth} from './auth/access.guard';
import {MainPageComponent} from './pages/main-page/main-page.component';
import {ContactPageComponent} from './pages/contact-page/contact-page.component';
import {ReservationPageComponent} from './pages/reservation-page/reservation-page.component';

export const routes: Routes = [
  {path: '', component: LayoutComponent, children: [
      {path: '', component: MainPageComponent},
      {path: 'contacts', component: ContactPageComponent},
      {path: 'reservation', component: ReservationPageComponent},
    ],
    canActivate: [canActivateAuth]
  },
  {path: 'login', component: LoginPageComponent},
  {path: 'register', component: RegisterPageComponent}
];
