import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { NotifyService } from '../_services/notify.service';
import { AnnouncementForList } from '../_models/announcementForList';
import { AnnouncementService } from '../_services/announcement.service';

@Injectable()
export class MyAnnouncementsResolver implements Resolve<AnnouncementForList[]> {
    pageNumber = 1;
    pageSize = 6;

    constructor(
        private announcementService: AnnouncementService,
        private router: Router,
        private notificationService: NotifyService,
    ) { }

    resolve(route: ActivatedRouteSnapshot): Observable<AnnouncementForList[]> {
        return this.announcementService.getMyAnnouncements(this.pageNumber, this.pageSize).pipe(
                catchError(_ => {
                    this.notificationService.error('Problem retrieving announcements');
                    this.router.navigate(['/']);
                    return of(null);
                })
            );
    }
}
