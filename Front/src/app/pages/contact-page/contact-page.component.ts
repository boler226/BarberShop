import {AfterViewInit, Component, inject, OnInit} from '@angular/core';
import {Affiliate} from '../../interfaces/contact/contact.interface';
import {AffiliateService} from '../../services/affiliate/affiliate.service';
import {LocationService} from '../../services/location.service';

@Component({
  selector: 'app-contact-page',
  imports: [],
  templateUrl: './contact-page.component.html',
  standalone: true,
  styleUrl: './contact-page.component.scss'
})
export class ContactPageComponent implements OnInit {
  contacts: Affiliate

  affiliateService: AffiliateService = inject(AffiliateService)
  locationService: LocationService = inject(LocationService)

  ngOnInit() {

  }

  fetchAffiliate(): void {
    const city = this.locationService.getCity()
    if (city) {
      this.affiliateService.getByCity(city.name).subscribe({
        next: (affiliate) => {
          this.contacts = affiliate
        },
        error: err => console.error('Failed to fetch affiliate:', err)
      })
    } else {
      console.warn('City is not selected')
    }
  }
}
