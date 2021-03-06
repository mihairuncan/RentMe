import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from '../_models/user';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  baseUrl: string;
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  currentUser: User;

  isLoggedIn = new BehaviorSubject<boolean>(this.loggedIn());
  userIsLoggedIn = this.isLoggedIn.asObservable();

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
  ) {
    this.baseUrl = baseUrl;
  }

  login(model: any) {
    return this.http.post(this.baseUrl + 'api/auth/login', model)
      .pipe(
        map((response: any) => {
          const user = response;
          if (user) {
            localStorage.setItem('token', user.token);
            localStorage.setItem('user', JSON.stringify(user.user));
            this.decodedToken = this.jwtHelper.decodeToken(user.token);
            this.currentUser = user.user;
            this.isLoggedIn.next(true);
          }
        })
      );
  }

  register(user: User) {
    return this.http.post(this.baseUrl + 'api/auth/register', user);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  resetPassword(userForPasswordRecorer: any) {
    return this.http.post(this.baseUrl + 'api/auth/forgotPassword', userForPasswordRecorer);
  }

  changePassword(passwordText: string) {
    return this.http.post(this.baseUrl + 'api/auth/changePassword', { passwordText });
  }

  roleMatch(allowedRoles): boolean {
    let isMatch = false;
    const userRoles = this.decodedToken.role as Array<string>;
    if (userRoles) {
      allowedRoles.forEach(element => {
        if (userRoles.includes(element)) {
          isMatch = true;
          return;
        }
      });
    }
    return isMatch;
  }
}
