import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DatabaseService {
  private apiUrl = 'http://localhost:7091/api/Database';

  constructor(private http: HttpClient) {}

  getBackup(): Observable<HttpResponse<Blob>> {
    return this.http.get(`${this.apiUrl}/export-inserts`, {
      observe: 'response',
      responseType: 'blob',
    });
  }

  loadBackup(sqlScript: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/import-inserts`, { sqlScript });
  }
}
