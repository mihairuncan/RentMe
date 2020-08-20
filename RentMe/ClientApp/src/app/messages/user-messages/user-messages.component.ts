import { Component, OnInit, EventEmitter, Output, AfterViewChecked } from '@angular/core';
import { Message } from 'src/app/_models/message';
import { AuthenticationService } from 'src/app/_services/auth.service';
import { tap } from 'rxjs/operators';
import { NotifyService } from 'src/app/_services/notify.service';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-user-messages',
  templateUrl: './user-messages.component.html',
  styleUrls: ['./user-messages.component.css']
})
export class UserMessagesComponent implements OnInit, AfterViewChecked {

  recipientId: string;
  messages: Message[];
  newMessage: any = {};
  currentUserId: string;

  @Output() newMessages = new EventEmitter();


  constructor(
    private messageService: MessageService,
    private authService: AuthenticationService,
    private notificationService: NotifyService,
  ) { }


  ngAfterViewChecked() {
    document.querySelector('.message-history').scroll(0, document.querySelector('.message-history').scrollHeight);
  }

  ngOnInit() {
    this.currentUserId = this.authService.decodedToken.nameid;
    this.messageService.recipientUserId.subscribe(recipientId => {
      this.recipientId = recipientId;
      if (recipientId) {
        this.loadMessages();
      }
    });

    this.messageService.messageReceived.subscribe((message: Message) => {
      if (message.recipientId === this.recipientId || message.senderId === this.recipientId) {
        this.messages.push(message);
        if (message.senderId === this.recipientId) {
          this.messageService.markAsRead(this.currentUserId, message.id);
        }
      }
      this.newMessages.emit();
    }, error => {
      this.notificationService.error(error);
    });

    this.messageService.messageRead.subscribe((readerUserId: string) => {
      if (readerUserId === this.recipientId) {
        this.loadMessages();
      }
    });
  }

  loadMessages() {
    this.messageService.getMessageThread(this.authService.decodedToken.nameid, this.recipientId)
      .pipe(
        tap(messages => {
          for (let i = 0; i < messages.length; i++) {
            if (messages[i].isRead === false && messages[i].recipientId === this.currentUserId) {
              this.messageService.markAsRead(this.currentUserId, messages[i].id);
            }
          }
        })
      )
      .subscribe(messages => {
        this.messages = messages;
      }, error => {
        this.notificationService.error(error);
      });
  }

  sendMessage() {
    this.newMessage.recipientId = this.recipientId;
    this.messageService.sendMessage(this.authService.decodedToken.nameid, this.newMessage)
      .subscribe((message: Message) => {
        this.newMessage.content = '';
      }, error => {
        this.notificationService.error(error);
      });
  }
}
