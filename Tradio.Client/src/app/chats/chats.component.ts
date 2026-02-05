import { Component, OnInit } from '@angular/core';
import { ChatModel } from '../core/responses/chat.model';
import { ApplicationUserServiceService } from '../core/services/application-user-service.service';
import { Observable } from 'rxjs';
import { ChatListItemModel } from '../core/responses/chat-list-item.model';
import { MessageService } from '../core/services/message.service';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'chats',
  templateUrl: './chats.component.html',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  host: { class: 'flex-row' },
})
export class ChatsComponent implements OnInit {
  constructor(
    private applicationUserService: ApplicationUserServiceService,
    private messageService: MessageService,
    private activatedRoute: ActivatedRoute,
    formBuilder: FormBuilder,
  ) {
    this.createMessageGroup = formBuilder.group({
      text: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.chatListItems = this.applicationUserService.getReceivedServiceChats();
    const serviceId = this.activatedRoute.snapshot.queryParams['serviceId'];
    if (!serviceId) {
      return;
    }
    this.chatModel = this.messageService.getMessagesByService(serviceId);
  }
  chatModel?: Observable<ChatModel>;
  chatListItems!: Observable<ChatListItemModel[]>;
  createMessageGroup: FormGroup;
  selectedChatId?: number;

  createMessage() {
    if (!this.createMessageGroup.valid) {
      return;
    }
    const messageText = this.createMessageGroup.value['text'];
    this.chatModel?.subscribe((chat) => {
      this.messageService
        .createMessage({
          text: messageText,
          serviceId: chat.serviceId,
          receiverId: chat.applicationUserId,
        })
        .subscribe(() => {
          this.createMessageGroup.reset();
        });
    });
  }

  selectChat(chatId: number) {
    this.chatModel = this.messageService.getMessagesByChat(chatId);
    this.selectedChatId = chatId;
  }

  onEnter(event: KeyboardEvent) {
    if (event.shiftKey) {
      return;
    }
    event.preventDefault();
    this.createMessage();
  }
}
