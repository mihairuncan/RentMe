import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthenticationService } from '../_services/auth.service';
import { User } from '../_models/user';
import { NotifyService } from '../_services/notify.service';
import { AdminService } from '../_services/admin.service';

@Injectable()
export class UsersResolver implements Resolve<User[]> {
    pageNumber = 1;
    pageSize = 5;
    searchUserInput = '';

    constructor(
        private adminService: AdminService,
        private authService: AuthenticationService,
        private router: Router,
        private notificationService: NotifyService,
        ) { }

    resolve(route: ActivatedRouteSnapshot): Observable<User[]> {
        return this.adminService.getUsersWithRoles(
                this.pageNumber, this.pageSize, this.searchUserInput).pipe(
            catchError(_ => {
                this.notificationService.error('Problem retrieving users');
                this.router.navigate(['/']);
                return of(null);
            })
        );
    }
}
