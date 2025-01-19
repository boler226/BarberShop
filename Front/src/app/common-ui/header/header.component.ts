import {Component, inject, OnInit} from '@angular/core';
import {NgForOf, NgIf, NgOptimizedImage} from '@angular/common';
import {Country} from '../../interfaces/country/country.interface';
import {City} from '../../interfaces/city/city.interface';
import {CountryService} from '../../services/country/country.service';
import {LocationService} from '../../services/location.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-header',
  imports: [
    NgOptimizedImage,
    NgIf,
    NgForOf
  ],
  templateUrl: './header.component.html',
  standalone: true,
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {
  countries: Country[] = []
  selectedCountry?: Country | null
  selectedCity?: City | null

  private countryService: CountryService = inject(CountryService)
  private locationService: LocationService = inject(LocationService)
  private router: Router = inject(Router)

  ngOnInit(): void {
    this.fetchCountries()
    this.subscribeToLocationChanges()
  }

  fetchCountries(): void {
    this.countryService.getAll().subscribe({
      next: (response) => {
        this.countries = response

        if (this.countries.length > 0) {
          this.selectCountry(this.countries[0])
        }
      },
      error: (err) => console.error('Fetch country failed:', err)
    })
  }

  selectCountry(country: Country): void {
    this.selectedCountry = country
    this.locationService.setCountry(country)
  }

  selectCity(city: City): void {
    this.selectedCity = city;
    this.locationService.setCity(city);
  }

  subscribeToLocationChanges(): void {
    this.locationService.selectedCity$.subscribe((city) => {
      if (this.isContactsPage() && city) {
        this.locationService.triggerAffiliateFetch(city.name)
      }
    })

    this.locationService.selectedCountry$.subscribe((country) => {
      if (this.isContactsPage() && country) {
        const defaultCity = country.cities[0]
        if (defaultCity) {
          this.locationService.triggerAffiliateFetch(defaultCity.name)
        }
      }
    })
  }

  private isContactsPage(): boolean {
    return this.router.url === '/contacts';
  }

  toContacts(): void {
    this.router.navigate(['/contacts'])
  }
}
