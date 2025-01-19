import { Injectable } from '@angular/core';
import {BehaviorSubject, Subject} from 'rxjs';
import {Country} from '../interfaces/country/country.interface';
import {City} from '../interfaces/city/city.interface';

@Injectable({
  providedIn: 'root'
})
export class LocationService {
  private selectedCountrySubject = new BehaviorSubject<Country | null>(null)
  private  selectedCitySubject = new BehaviorSubject<City | null>(null)
  private affiliateFetchTrigger = new Subject<string>()

  selectedCountry$ = this.selectedCountrySubject.asObservable()
  selectedCity$ = this.selectedCitySubject.asObservable()
  affiliateFetchTrigger$ = this.affiliateFetchTrigger.asObservable()


  setCity(city: City): void {
    this.selectedCitySubject.next(city)
  }

  setCountry(country: Country): void {
    this.selectedCountrySubject.next(country)
    if (country.cities.length > 0) {
      this.setCity(country.cities[0])
    }
  }

  triggerAffiliateFetch(cityName: string): void {
    this.affiliateFetchTrigger.next(cityName)
  }

  getCity(): City | null {
    return this.selectedCitySubject.value;
  }
}
