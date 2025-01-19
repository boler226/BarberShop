import {AfterViewInit, Component, inject, OnInit} from '@angular/core';
import {Affiliate} from '../../interfaces/contact/contact.interface';
import {AffiliateService} from '../../services/affiliate/affiliate.service';
import {LocationService} from '../../services/location.service';
import {distinctUntilChanged} from 'rxjs';
import {JsonPipe, NgForOf, NgIf} from '@angular/common';
import {Router} from '@angular/router';

@Component({
  selector: 'app-contact-page',
  imports: [
    NgIf,
    NgForOf,
    JsonPipe
  ],
  templateUrl: './contact-page.component.html',
  standalone: true,
  styleUrl: './contact-page.component.scss'
})
export class ContactPageComponent implements OnInit {
  contacts!: Affiliate

  affiliateService: AffiliateService = inject(AffiliateService)
  locationService: LocationService = inject(LocationService)
  private router = inject(Router)

  ngOnInit() {
    this.locationService.affiliateFetchTrigger$.pipe(
      distinctUntilChanged()
    ).subscribe((cityName) => {
      this.fetchAffiliate(cityName)
    })
  }

  fetchAffiliate(cityName: string): void {
    this.affiliateService.getByName(cityName).subscribe({
      next: (affiliate) => {
        this.contacts = affiliate
        console.log("Contacts: ", this.contacts)
      },
      error: (err) => console.error('Failed to fetch affiliate:', err),
    })
  }

  onBarbershopClick(barbershop: any): void {
    this.router.navigate(['/reservation'], {
      state: { barbershop }
    })
  }
}
