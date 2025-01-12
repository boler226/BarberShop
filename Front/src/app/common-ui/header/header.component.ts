import {Component, inject, OnInit} from '@angular/core';
import {NgForOf, NgIf, NgOptimizedImage} from '@angular/common';
import {City, Country} from '../../interfaces/country/country.interface';
import {CountryService} from '../../services/country.service';

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
  selectedCountry?: Country
  selectedCity?: City

  countryService: CountryService = inject(CountryService)

  ngOnInit(): void {
    this.fetchCountries()
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
      error: (err) => console.error('Fetch countries failed:', err)
    })
  }

  selectCountry(country: Country): void {
    this.selectedCountry = country
    this.selectedCity = country.cities[0]
  }

  selectCity(city: City): void {
    this.selectedCity = city
  }
}
