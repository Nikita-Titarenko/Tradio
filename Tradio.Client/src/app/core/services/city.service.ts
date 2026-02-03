import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CityModel } from '../responses/city.model';

@Injectable({
  providedIn: 'root',
})
export class CityService {
  private apiUrl = 'http://localhost:5188/api/Cities';

  constructor(private http: HttpClient) {}

  getCities(countryId?: number): Observable<CityModel[]> {
    let params = new HttpParams();
    if (countryId !== undefined) {
      params = params.set('countryId', countryId.toString());
    }
    return this.http.get<CityModel[]>(`${this.apiUrl}`, { params });
  }
}
