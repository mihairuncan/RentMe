import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthenticationService } from '../_services/auth.service';
import { NotifyService } from '../_services/notify.service';
import { AdminService } from '../_services/admin.service';
import { AnnouncementForList } from '../_models/announcementForList';

@Injectable()
export class UnapprovedAnnouncementsResolver implements Resolve<AnnouncementForList[]> {
    pageNumber = 1;
    pageSize = 6;

    constructor(
        private adminService: AdminService,
        private authService: AuthenticationService,
        private router: Router,
        private notificationService: NotifyService,
        ) { }

    resolve(route: ActivatedRouteSnapshot): Observable<AnnouncementForList[]> {
        return this.adminService.getUnapprovedAnnouncements(
                this.pageNumber, this.pageSize).pipe(
            catchError(_ => {
                this.notificationService.error('Problem retrieving unapproved announcements');
                this.router.navigate(['/']);
                return of(null);
            })
        );
    }
}
