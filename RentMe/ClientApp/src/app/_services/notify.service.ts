import { Injectable } from '@angular/core';
import { NotificationsService } from 'angular2-notifications';

@Injectable({
  providedIn: 'root'
})
export class NotifyService {

  constructor(
    private notificationService: NotificationsService,
  ) { }

  success(title: string, message?: string) {
    this.notificationService.success(title, message, {
      timeOut: 1000,
      showProgressBar: false,
      pauseOnHover: true,
      clickToClose: true,
      animate: 'scale'
    });
  }

  error(title: string, message?: string) {
    this.notificationService.error(title, message, {
      timeOut: 1000,
      showProgressBar: false,
      pauseOnHover: true,
      clickToClose: true,
      animate: 'scale'
    });
  }

  alert(title: string, message?: string) {
    this.notificationService.alert(title, message, {
      timeOut: 1000,
      showProgressBar: false,
      pauseOnHover: true,
      clickToClose: true,
      animate: 'scale'
    });
  }

  warn(title: string, message?: string) {
    this.notificationService.warn(title, message, {
      timeOut: 1000,
      showProgressBar: false,
      pauseOnHover: true,
      clickToClose: true,
      animate: 'scale'
    });
  }

  info(title: string, message?: string) {
    this.notificationService.info(title, message, {
      timeOut: 1000,
      showProgressBar: false,
      pauseOnHover: true,
      clickToClose: true,
      animate: 'scale'
    });
  }
}
