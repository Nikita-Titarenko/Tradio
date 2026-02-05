import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ChatModel } from '../responses/chat.model';
import { CreateMessageModel } from '../requests/create-message.model';

@Injectable({
  providedIn: 'root',
})
export class MessageService {
  private apiUrl = 'http://localhost:5188/api/Messages';

  constructor(private http: HttpClient) {}

  getMessagesByService(serviceId: number): Observable<ChatModel> {
    const params = new HttpParams().set('serviceId', serviceId.toString());
    return this.http.get<ChatModel>(`${this.apiUrl}/by-service`, { params });
  }

  getMessagesByChat(chatId: number): Observable<ChatModel> {
    const params = new HttpParams().set(
      'applicationUserServiceId',
      chatId.toString(),
    );
    return this.http.get<ChatModel>(
      `${this.apiUrl}/by-application-user-service`,
      { params },
    );
  }

  createMessage(messageModel: CreateMessageModel): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}`, messageModel);
  }
}
