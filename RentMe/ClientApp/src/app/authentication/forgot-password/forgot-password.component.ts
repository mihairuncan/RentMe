import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NotificationsService } from 'angular2-notifications';
import { AuthenticationService } from '../../_services/auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { trigger, transition, style, animate } from '@angular/animations';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css'],
  animations: [
    trigger('fade', [
      transition('void => *', [
        style({ opacity: 0 }),
        animate(2000)
      ])
    ])
  ]
})
export class ForgotPasswordComponent implements OnInit {
  forgotPasswordForm: FormGroup;
  isSubmitting = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthenticationService,
    private notifyService: NotificationsService,
    private router: Router
  ) { }

  ngOnInit() {
    this.createForgotPasswordForm();
  }

  createForgotPasswordForm() {
    this.forgotPasswordForm = this.fb.group({
      username: [''],
      email: ['', Validators.email]
    });
  }

  resetPassword() {
    this.isSubmitting = true;
    this.authService.resetPassword(this.forgotPasswordForm.value).subscribe(() => {
      this.router.navigateByUrl('/login');
      this.notifyService.success('Check your mail for the new password');
    }, error => {
      this.notifyService.error(error);
    });
  }

}
