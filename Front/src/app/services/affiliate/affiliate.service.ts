import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Affiliate} from '../../interfaces/contact/contact.interface';

@Injectable({
  providedIn: 'root'
})
export class AffiliateService {
  baseUrl: string = 'https://localhost:7142/api/Affiliates/GetByName'
  http: HttpClient = inject(HttpClient)

  getByName(city: string | null): Observable<Affiliate> {
    return this.http.get<Affiliate>(`${this.baseUrl}/${city}`)
  }
}
