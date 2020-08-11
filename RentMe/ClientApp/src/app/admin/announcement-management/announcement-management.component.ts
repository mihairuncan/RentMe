import { Component, OnInit } from '@angular/core';
import { NotifyService } from 'src/app/_services/notify.service';
import { AdminService } from 'src/app/_services/admin.service';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';
import { AuthenticationService } from 'src/app/_services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { Announcement } from 'src/app/_models/announcement';
import { AnnouncementForList } from 'src/app/_models/announcementForList';

@Component({
  selector: 'app-announcement-management',
  templateUrl: './announcement-management.component.html',
  styleUrls: ['./announcement-management.component.css']
})
export class AnnouncementManagementComponent implements OnInit {
  announcements: AnnouncementForList[];
  pagination: Pagination;
  numberOfPages = 3;

  constructor(
    private notifyService: NotifyService,
    private adminService: AdminService,
    private route: ActivatedRoute,
  ) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.announcements = data['announcements'].result;
      this.pagination = data['announcements'].pagination;
    });
  }

  loadAnnouncements() {
    this.adminService.getUnapprovedAnnouncements(
      this.pagination.currentPage,
      this.pagination.itemsPerPage
    )
      .subscribe((res: PaginatedResult<AnnouncementForList[]>) => {
        this.announcements = res.result;
        this.pagination = res.pagination;
      }, error => {
        this.notifyService.error(error);
      });
  }


  pageChange(pageNumber: number): void {
    this.pagination.currentPage = pageNumber;
    this.loadAnnouncements();
  }

  approveAnnouncement(announcementId: string) {
    this.adminService.approveAnnouncement(announcementId).subscribe(() => {
      this.announcements.splice(this.announcements.findIndex(a => a.id === announcementId), 1);
      this.loadAnnouncements();
      this.notifyService.success('Announcement approved');
    }, error => {
      this.notifyService.error(error);
    });
  }

  rejectAnnouncement(announcementId: string) {
    this.adminService.rejectAnnouncement(announcementId).subscribe(() => {
      this.announcements.splice(this.announcements.findIndex(a => a.id === announcementId), 1);
      this.loadAnnouncements();
      this.notifyService.success('Announcement rejected');
    }, error => {
      this.notifyService.error(error);
    });
  }
}
