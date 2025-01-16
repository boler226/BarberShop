import {Component, inject, OnInit} from '@angular/core';
import {NgForOf, NgIf, NgOptimizedImage} from '@angular/common';
import {City, Country} from '../../interfaces/country/country.interface';
import {CountryService} from '../../services/country/country.service';
import {LocationService} from '../../services/location.service';

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
          this.selectCity(this.countries[0].cities[0])
        }
      },
      error: (err) => console.error('Fetch country failed:', err)
    })
  }

  selectCountry(country: Country): void {
    this.selectedCountry = country
    this.selectedCity = country.cities[0]
  }

  selectCity(city: City): void {
    this.selectedCity = city
  }

  subscribeToLocationChanges(): void {
    this.locationService.selectedCountry$.subscribe((country) => {
      this.selectedCountry = country
    })

    this.locationService.selectedCity$.subscribe((city)=> {
      this.selectedCity = city
    })
  }
}
