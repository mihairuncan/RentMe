import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
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
export class UserMessagesComponent implements OnInit {

  recipientId: string;
  messages: Message[];
  newMessage: any = {};

  @Output() newMessages = new EventEmitter();


  constructor(
    private messageService: MessageService,
    private authService: AuthenticationService,
    private notificationService: NotifyService,
  ) { }

  ngOnInit() {
    this.messageService.recipientUserId.subscribe(recipientId => {
      this.recipientId = recipientId;
      if (recipientId) {
        this.loadMessages();
      }
    });

    this.messageService.messageReceived.subscribe((message: Message) => {
      // this.messages.unshift(message);
      this.loadMessages();
      this.newMessages.emit();
    });
  }

  loadMessages() {
    const currentUserId = this.authService.decodedToken.nameid;
    this.messageService.getMessageThread(this.authService.decodedToken.nameid, this.recipientId)
      .pipe(
        tap(messages => {
          for (let i = 0; i < messages.length; i++) {
            if (messages[i].isRead === false && messages[i].recipientId === currentUserId) {
              this.messageService.markAsRead(currentUserId, messages[i].id);
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
        // this.messages.unshift(message);
        this.newMessage.content = '';
      }, error => {
        this.notificationService.error(error);
      });
  }



}
