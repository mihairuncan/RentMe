import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../_services/auth.service';
import { NotifyService } from '../_services/notify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;

  constructor(
    private authService: AuthenticationService,
    private notificationService: NotifyService,
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout() {
    this.collapse();
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.decodedToken = null;
    this.authService.currentUser = null;
    this.authService.isLoggedIn.next(false);
    this.notificationService.info('Logged out');
    this.router.navigate(['']);
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  openMyMessages() {
    this.collapse();
    this.router.navigate(['myMessages']);
  }

  openMyAnnouncements() {
    this.collapse();
    this.router.navigate(['myAnnouncements']);
  }

  openAdminPanel() {
    this.collapse();
    this.router.navigate(['admin']);
  }

  openLogin() {
    this.collapse();
    this.router.navigate(['login']);
  }

  openRegister() {
    this.collapse();
    this.router.navigate(['register']);
  }
}
