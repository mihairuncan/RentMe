import { Component, OnInit } from '@angular/core';
import { NotificationsService } from 'angular2-notifications';
import { AuthenticationService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { NotifyService } from '../_services/notify.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: any = {};

  constructor(
    private notificationService:NotifyService,
    private authService: AuthenticationService,
    private router: Router
  ) { }


  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.notificationService.success('Successfully Logged In', '');
    }, error => {
      this.notificationService.error('Error', error);
    }, () => {
      this.router.navigate(['']);
    });
  }
}
