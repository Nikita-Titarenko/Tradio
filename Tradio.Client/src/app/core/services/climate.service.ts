import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ClimateStatisticModel } from '../responses/climate-statistic.model';

@Injectable({
  providedIn: 'root',
})
export class ClimateService {
  private apiUrl = 'http://localhost:7091/api/Climates';

  constructor(private http: HttpClient) {}

  getClimateStatistic(): Observable<ClimateStatisticModel> {
    return this.http.get<ClimateStatisticModel>(`${this.apiUrl}`);
  }
}
