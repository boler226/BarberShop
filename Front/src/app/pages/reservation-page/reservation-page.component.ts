import {Component, inject} from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-reservation-page',
  imports: [],
  templateUrl: './reservation-page.component.html',
  standalone: true,
  styleUrl: './reservation-page.component.scss'
})
export class ReservationPageComponent {
  affiliate: any

  private router = inject(Router)

  constructor() {
    const navigation = this.router.getCurrentNavigation()
    this.affiliate = navigation?.extras.state?.['barbershop']

    // if (!this.affiliate) {
    //   console.error('This data is uncorrected!')
    //   this.router.navigate(['/contacts'])
    // }
  }
}
