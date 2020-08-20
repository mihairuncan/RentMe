import { Injectable, Inject, EventEmitter } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';
import { Message } from '../_models/message';
import { BehaviorSubject } from 'rxjs';
import { MessageForList } from '../_models/messageForList';
import * as signalR from '@aspnet/signalr';
import { AuthenticationService } from './auth.service';


@Injectable({
  providedIn: 'root'
})
export class MessageService {
  baseUrl: string;

  recipientId = new BehaviorSubject<string>('');
  recipientUserId = this.recipientId.asObservable();

  private hubConnection: signalR.HubConnection;
  messageReceived = new EventEmitter<Message>();
  messageRead = new EventEmitter<string>();


  constructor(
    private authService: AuthenticationService,
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.baseUrl = baseUrl;
    this.buildConnection();
    this.startConnection();
  }

  setRecipientId(recipientId: string) {
    this.recipientId.next(recipientId);
  }


  getMessageThread(userId: string, recipientId: string) {
    return this.http.get<Message[]>(this.baseUrl + 'api/users/' + userId + '/messages/thread/' + recipientId);
  }

  sendMessage(id: string, message: Message) {
    return this.http.post(this.baseUrl + 'api/users/' + id + '/messages', message);
  }

  deleteMessage(id: string, userId: string) {
    return this.http.post(this.baseUrl + 'api/users/' + userId + '/messages/' + id, {});
  }

  markAsRead(userId: string, messageId: string) {
    this.http.post(this.baseUrl + 'api/users/' + userId + '/messages/' + messageId + '/read', {})
      .subscribe();
  }

  getMessages(id: string, page?, itemsPerPage?, messageContainer?) {
    const paginatedResult: PaginatedResult<Message[]> = new PaginatedResult<Message[]>();
    let params = new HttpParams();

    params = params.append('MessageContainer', messageContainer);

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<Message[]>(this.baseUrl + 'api/users/' + id + '/messages', { observe: 'response', params })
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          return paginatedResult;
        })
      );
  }

  getMessagesList(userId: string) {
    return this.http.get<MessageForList[]>(this.baseUrl + 'api/users/' + userId + '/messages/list');
  }

  public buildConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://10.0.0.5:5001/messageHub/?userId=' + this.authService.decodedToken.nameid)
      .build();
  }

  public startConnection = () => {
    this.hubConnection
      .start()
      .then(() => {
        console.log('Connection started...');
        this.registerSignalEvents();
      })
      .catch(err => {
        console.log('Error while starting connection: ' + err);

        setTimeout(function (): any { this.startConnection(); }, 3000);
      });
  }

  private registerSignalEvents(): any {
    this.hubConnection.on('SignalMessageReceived', (data: Message) => {
      console.log(data);
      this.messageReceived.emit(data);
    });

    this.hubConnection.on('MessageRead', (readerUserId: string) => {
      console.log(readerUserId);
      this.messageRead.emit(readerUserId);
    });

  }

}
