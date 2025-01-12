import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Country} from '../interfaces/country/country.interface';

@Injectable({
  providedIn: 'root'
})
export class CountryService {
  baseUrl: string = 'https://localhost:7142/api/Countries/';
  http: HttpClient = inject(HttpClient);

  getAll(): Observable<Country[]> {
    return  this.http.get<Country[]>(`${this.baseUrl}GetAll`);
  }
}
