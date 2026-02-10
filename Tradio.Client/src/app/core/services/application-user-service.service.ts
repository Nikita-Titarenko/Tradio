import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ChatListItemModel } from '../responses/chat-list-item.model';

@Injectable({
  providedIn: 'root',
})
export class ApplicationUserServiceService {
  private apiUrl = 'http://localhost:5188/api/ApplicationUserServices';

  constructor(private http: HttpClient) {}

  getReceivedServiceChats(): Observable<ChatListItemModel[]> {
    return this.http.get<ChatListItemModel[]>(
      `${this.apiUrl}/received-service`,
    );
  }

  getProvidedServiceChats(): Observable<ChatListItemModel[]> {
    return this.http.get<ChatListItemModel[]>(
      `${this.apiUrl}/provided-service`,
    );
  }
}
