import { Injectable } from '@angular/core';
import {BehaviorSubject} from 'rxjs';
import {Country} from '../interfaces/country/country.interface';
import {City} from '../interfaces/contact/contact.interface';

@Injectable({
  providedIn: 'root'
})
export class LocationService {
  private selectedCountrySubject = new BehaviorSubject<Country | null>(null)
  private  selectedCitySubject = new BehaviorSubject<City | null>(null)

  selectedCountry$ = this.selectedCountrySubject.asObservable()
  selectedCity$ = this.selectedCitySubject.asObservable()

  setCountry(country: Country): void {
    this.selectedCountrySubject.next(country)
    if (country.cities.length > 0) {
      this.setCity(<City>country.cities[0])
    }
  }

  setCity(city: City): void {
    this.selectedCitySubject.next(city)
  }

  getCountry(): Country | null {
    return this.selectedCountrySubject.value
  }

  getCity(): City | null {
    return this.selectedCitySubject.value
  }
}
