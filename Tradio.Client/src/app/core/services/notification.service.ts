import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Subject } from 'rxjs/internal/Subject';
import { MessageFromSingalRModel } from '../responses/message-from-singal-r.model';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  private hubConnection!: HubConnection;
  private messageSubject = new Subject<MessageFromSingalRModel>();
  messages$ = this.messageSubject.asObservable();

  async start(userId: string) {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('https://localhost:7171/chatHub')
      .withAutomaticReconnect()
      .build();

    this.hubConnection.on(
      'ReceiveMessage',
      (message: MessageFromSingalRModel) => {
        this.messageSubject.next(message);
      },
    );

    await this.hubConnection.start();
    await this.joinUser(userId);
  }

  async joinToChats(chatIds: number[]) {
    for (let chatId of chatIds) {
      await this.hubConnection.invoke('AddToChatAsync', chatId);
    }
  }

  private async joinUser(userId: string) {
    await this.hubConnection.invoke('AddUserAsync', userId);
  }
}
