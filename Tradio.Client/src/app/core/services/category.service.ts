import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CategoryModel } from '../responses/category.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private apiUrl = 'http://localhost:5188/api/Categories';

  constructor(private http: HttpClient) { }

  getCategories(parentCategoryId?: number): Observable<CategoryModel[]> {
    let params = new HttpParams();
    if (parentCategoryId !== undefined) {
      params = params.set('parentCategoryId', parentCategoryId.toString());
    }
    return this.http.get<CategoryModel[]>(`${this.apiUrl}`, { params });
  }

}
