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
  BehaviorSubject,
  from,
  map,
  merge,
  Observable,
  of,
  ReplaySubject,
  scan,
  startWith,
  switchMap,
  take,
  tap,
} from 'rxjs';
import { ChatListItemModel } from '../core/responses/chat-list-item.model';
import { ChatModel } from '../core/responses/chat.model';
import { ApplicationUserServiceService } from '../core/services/application-user-service.service';
import { UserService } from '../core/services/user.service';
import { MessageService } from '../core/services/message.service';
import { NotificationService } from '../core/services/notification.service';
import { PaymentService } from '../core/services/payment.service';
import { MessageModel } from '../core/responses/message.model';

@Component({
  selector: 'chats',
  templateUrl: './chats.component.html',
  styleUrls: ['./chats.component.css'],
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  host: { class: 'flex-row' },
})
export class ChatsComponent implements OnInit, AfterViewChecked {
  private chatSubject = new ReplaySubject<ChatModel>(1);
  // Потік, який об'єднує початкові дані чату та нові повідомлення з SignalR
  chatModel$ = this.chatSubject.asObservable().pipe(
    switchMap((initialChat) =>
      this.notificationService.messages$.pipe(
        scan((accChat: ChatModel, newMessage) => {
          this.shouldScroll = true;

          // Уникаємо дублікатів, якщо повідомлення прийшло і від API, і через SignalR
          const exists = accChat.messages.some(
            (m) =>
              m.creationDateTime === newMessage.creationDateTime &&
              m.text === newMessage.text,
          );
          if (exists) return accChat;

          return {
            ...accChat,
            messages: [
              ...accChat.messages,
              {
                text: newMessage.text,
                isYourMessage: newMessage.senderId === this.authService.userId,
                isRead: false,
                creationDateTime: newMessage.creationDateTime,
                applicationUserServiceId: newMessage.applicationUserServiceId,
              },
            ],
          };
        }, initialChat),
        startWith(initialChat),
      ),
    ),
  );

  chatListItems!: Observable<ChatListItemModel[]>;
  createMessageGroup: FormGroup;
  filterChatGroup: FormGroup;
  @ViewChild('messagesContainer') private messagesContainer!: ElementRef;
  private shouldScroll: boolean = false;

  constructor(
    private applicationUserService: ApplicationUserServiceService,
    private messageService: MessageService,
    private activatedRoute: ActivatedRoute,
    private notificationService: NotificationService,
    private authService: UserService,
    private paymentService: PaymentService,
    formBuilder: FormBuilder,
  ) {
    this.createMessageGroup = formBuilder.group({
      text: ['', Validators.required],
    });
    this.filterChatGroup = formBuilder.group({
      chatType: ['received'],
    });
  }

  ngAfterViewChecked(): void {
    if (this.shouldScroll) {
      this.shouldScroll = false;
      this.scrollToBottom();
    }
  }

  private scrollToBottom() {
    if (this.messagesContainer) {
      const container = this.messagesContainer.nativeElement;
      container.scrollTop = container.scrollHeight;
    }
  }

  async ngOnInit(): Promise<void> {
    const userId = this.authService.userId;
    await this.notificationService.start(userId);

    this.chatListItems = this.filterChatGroup.valueChanges.pipe(
      startWith(this.filterChatGroup.value),
      switchMap((value) =>
        value.chatType === 'received'
          ? this.applicationUserService.getReceivedServiceChats()
          : this.applicationUserService.getProvidedServiceChats(),
      ),
      tap((chats) => {
        const ids = chats.map((c) => c.id).filter((id) => !!id);
        this.notificationService.joinToChats(ids);
      }),
    );

    const serviceId = this.activatedRoute.snapshot.queryParams['serviceId'];
    if (serviceId) {
      this.messageService.getMessagesByService(serviceId).subscribe((chat) => {
        this.shouldScroll = true;
        this.chatSubject.next(chat);
      });
    }
  }

  createMessage() {
    if (!this.createMessageGroup.valid) {
      return;
    }

    const messageText = this.createMessageGroup.get('text')?.value;

    this.chatModel$
      ?.pipe(
        take(1),

        switchMap((chat) =>
          this.messageService
            .createMessage({
              text: messageText,

              serviceId: chat.serviceId,

              receiverId: chat.applicationUserId,
            })
            .pipe(
              map((message) => {
                return { chat, message };
              }),
            ),
        ),

        tap(() => this.createMessageGroup.reset()),

        switchMap(({ chat, message }) => {
          if (chat.applicationUserServiceId) {
            return of(chat);
          }

          chat.applicationUserServiceId = message.applicationUserServiceId;

          const newMessage: MessageModel = {
            text: messageText,
            isYourMessage: true,
            isRead: false,
            creationDateTime: new Date().toISOString(),
            applicationUserServiceId: message.applicationUserServiceId,
          };

          const updatedChat = {
            ...chat,
            messages: [newMessage],
            applicationUserServiceId: message.applicationUserServiceId,
          };

          this.chatSubject.next(updatedChat);

          return from(
            this.notificationService.joinToChats([
              message.applicationUserServiceId,
            ]),
          );
        }),
      )
      .subscribe();
  }

  selectChat(chatId: number) {
    this.messageService.getMessagesByChat(chatId).subscribe((chat) => {
      this.shouldScroll = true;
      this.chatSubject.next(chat);
    });
  }

  onEnter(event: Event) {
    const keyboardEvent = event as KeyboardEvent;
    if (keyboardEvent.shiftKey) return;
    event.preventDefault();
    this.createMessage();
  }

  createPayment() {
    this.chatSubject.pipe(take(1)).subscribe((chat) => {
      if (!chat.applicationUserServiceId) return;
      this.paymentService
        .createPayment({
          applicationUserServiceId: chat.applicationUserServiceId,
        })
        .subscribe(() => this.authService.getUser().subscribe());
    });
  }
}
