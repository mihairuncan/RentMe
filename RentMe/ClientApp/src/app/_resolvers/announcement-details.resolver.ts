import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { NotifyService } from '../_services/notify.service';
import { AnnouncementService } from '../_services/announcement.service';
import { Announcement } from '../_models/announcement';

@Injectable()
export class AnnouncementResolver implements Resolve<Announcement> {
    constructor(
        private announcementService: AnnouncementService,
        private router: Router,
        private notificationService: NotifyService,
    ) { }

    resolve(route: ActivatedRouteSnapshot): Observable<Announcement> {
        return this.announcementService.getAnnouncement(route.params['announcementId']).pipe(
                catchError(_ => {
                    this.notificationService.error('Problem retrieving announcement\'s details');
                    this.router.navigate(['/']);
                    return of(null);
                })
            );
    }
}
