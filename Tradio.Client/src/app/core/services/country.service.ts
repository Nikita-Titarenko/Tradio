import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CountryModel } from '../responses/country.model';

@Injectable({
  providedIn: 'root',
})
export class CountryService {
  private apiUrl = 'http://localhost:5188/api/Countries';

  constructor(private http: HttpClient) {}

  getCountries(): Observable<CountryModel[]> {
    return this.http.get<CountryModel[]>(`${this.apiUrl}`);
  }
}
