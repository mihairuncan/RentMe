import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AuthenticationService } from '../_services/auth.service';
import { NotifyService } from '../_services/notify.service';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    constructor(
        private authService: AuthenticationService,
        private router: Router,
        private notificationService: NotifyService,
    ) { }

    canActivate(next: ActivatedRouteSnapshot): boolean {
        const roles = next.firstChild.data['roles'] as Array<string>;
        if (roles) {
            const math = this.authService.roleMatch(roles);
            if (math) {
                return true;
            } else {
                this.router.navigate(['']);
                this.notificationService.error('You are not authorized to access this area');
            }
        }
        if (this.authService.loggedIn()) {
            return true;
        }
        this.notificationService.error('You shall not pass!!!');
        this.router.navigate(['']);
        return false;
    }
}
