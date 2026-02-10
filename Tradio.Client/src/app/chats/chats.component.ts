import { CommonModule } from '@angular/common';
import {
  AfterViewChecked,
  Component,
  ElementRef,
  OnInit,
  ViewChild,
} from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, RouterLink } from '@angular/router';
import {
  from,
  map,
  merge,
  Observable,
  of,
  scan,
  startWith,
  switchMap,
  take,
} from 'rxjs';
import { ChatListItemModel } from '../core/responses/chat-list-item.model';
import { ChatModel } from '../core/responses/chat.model';
import { ApplicationUserServiceService } from '../core/services/application-user-service.service';
import { AuthService } from '../core/services/auth.service';
import { MessageService } from '../core/services/message.service';
import { NotificationService } from '../core/services/notification.service';

@Component({
  selector: 'chats',
  templateUrl: './chats.component.html',
  styleUrls: ['./chats.component.css'],
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  host: { class: 'flex-row' },
})
export class ChatsComponent implements OnInit, AfterViewChecked {
  constructor(
    private applicationUserService: ApplicationUserServiceService,
    private messageService: MessageService,
    private activatedRoute: ActivatedRoute,
    private notificationService: NotificationService,
    private authService: AuthService,
    formBuilder: FormBuilder,
  ) {
    this.createMessageGroup = formBuilder.group({
      text: ['', Validators.required],
    });
    this.filterChatGroup = formBuilder.group({
      chatType: ['received'],
    });
  }
  @ViewChild('messagesContainer') private messagesContainer!: ElementRef;
  private shouldScroll: boolean = false;
  ngAfterViewChecked(): void {
    if (this.shouldScroll) {
      this.shouldScroll = false;
      this.scrollToBottom();
    }
  }

  private scrollToBottom() {
    const container = this.messagesContainer.nativeElement;
    container.scrollTop = container.scrollHeight;
  }

  async ngOnInit(): Promise<void> {
    this.chatListItems = this.filterChatGroup.valueChanges.pipe(
      startWith(this.filterChatGroup.value),
      switchMap((value) => {
        if (value.chatType === 'received') {
          return this.applicationUserService.getReceivedServiceChats();
        } else if (value.chatType === 'provided') {
          return this.applicationUserService.getProvidedServiceChats();
        }
        return of();
      }),
    );

    const userId = this.authService.userId;
    await this.notificationService.start(userId);
    this.chatListItems
      .pipe(
        switchMap((chat) =>
          from(this.notificationService.joinToChats(chat.map((c) => c.id))),
        ),
      )
      .subscribe();

    const serviceId = this.activatedRoute.snapshot.queryParams['serviceId'];
    if (!serviceId) {
      return;
    }
    this.chatModel = this.messageService
      .getMessagesByService(serviceId)
      .pipe(this.listenMessages());
  }
  chatModel?: Observable<ChatModel>;
  chatListItems!: Observable<ChatListItemModel[]>;
  createMessageGroup: FormGroup;
  filterChatGroup: FormGroup;

  listenMessages() {
    return switchMap((chat: ChatModel) =>
      merge(
        of(chat).pipe(
          map((chat) => {
            this.shouldScroll = true;
            return chat;
          }),
        ),
        this.notificationService.messages$.pipe(
          scan((accChat: ChatModel, newMessage) => {
            this.shouldScroll = true;
            return {
              ...accChat,
              messages: [
                ...accChat.messages,
                {
                  text: newMessage.text,
                  isYourMessage:
                    newMessage.senderId === this.authService.userId,
                  isRead: false,
                  creationDateTime: newMessage.creationDateTime,
                },
              ],
            };
          }, chat),
        ),
      ),
    );
  }

  createMessage() {
    if (!this.createMessageGroup.valid) {
      return;
    }
    const messageText = this.createMessageGroup.value['text'];
    this.chatModel?.pipe(take(1)).subscribe((chat) => {
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
    this.chatModel = this.messageService
      .getMessagesByChat(chatId)
      .pipe(this.listenMessages());
  }

  onEnter(event: Event) {
    const keyboardEvent = event as KeyboardEvent;
    if (keyboardEvent.shiftKey) {
      return;
    }
    event.preventDefault();
    this.createMessage();
  }
}
