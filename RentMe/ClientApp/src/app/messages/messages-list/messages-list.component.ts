import { Component, OnInit } from '@angular/core';
import { MessageService } from 'src/app/_services/message.service';
import { NotifyService } from 'src/app/_services/notify.service';
import { AuthenticationService } from 'src/app/_services/auth.service';
import { MessageForList } from 'src/app/_models/messageForList';

@Component({
  selector: 'app-messages-list',
  templateUrl: './messages-list.component.html',
  styleUrls: ['./messages-list.component.css']
})
export class MessagesListComponent implements OnInit {
  messagesList: MessageForList[];
  selectedUsername: string;
  recipientId: string;

  constructor(
    private messageService: MessageService,
    private authService: AuthenticationService,
    private notificationService: NotifyService,
  ) { }

  ngOnInit() {
    this.loadMessagesList();
  }

  loadMessagesList() {
    this.messageService.getMessagesList(this.authService.decodedToken.nameid).subscribe(messages => {
      this.messagesList = messages;

      // if (!this.selectedUsername) {
      this.selectedUsername = this.messagesList[0].userName;
      this.recipientId = this.messagesList[0].userId;
      // }

      this.messageService.setRecipientId(this.recipientId);
    }, error => {
      this.notificationService.error(error);
    });

  }

  selectUser(message: MessageForList) {
    if (this.selectedUsername !== message.userName) {
      this.messageService.setRecipientId(message.userId);
      this.selectedUsername = message.userName;
    }
  }

  refreshMessages() {
    this.loadMessagesList();
  }
}
