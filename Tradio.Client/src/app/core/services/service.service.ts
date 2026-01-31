import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ServiceListItemModule } from '../responses/service-list-item.model';

@Injectable({
  providedIn: 'root',
})
export class ServiceService {
  private apiUrl = 'http://localhost:5188/api/Services';

  constructor(private http: HttpClient) {}

  getServices(
    pageNumber: number,
    pageSize: number,
    options?: {
      categoryId?: number;
      countryId?: number;
      cityId?: number;
      subName?: string;
    },
  ): Observable<ServiceListItemModule[]> {
    let params = new HttpParams();
    params = params
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    if (options != null) {
      Object.entries(options).forEach(([key, value]) => {
        if (value !== undefined) {
          params = params.set(key, value.toString());
        }
      });
    }

    return this.http.get<ServiceListItemModule[]>(`${this.apiUrl}`, { params });
  }
}
