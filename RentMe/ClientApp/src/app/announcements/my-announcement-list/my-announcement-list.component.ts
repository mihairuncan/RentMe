import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';
import { AnnouncementForList } from 'src/app/_models/announcementForList';
import { NotifyService } from 'src/app/_services/notify.service';
import { AnnouncementService } from 'src/app/_services/announcement.service';
import { Location } from '@angular/common';
import { AdminService } from 'src/app/_services/admin.service';

@Component({
  selector: 'app-my-announcement-list',
  templateUrl: './my-announcement-list.component.html',
  styleUrls: ['./my-announcement-list.component.css']
})
export class MyAnnouncementListComponent implements OnInit {
  announcements: AnnouncementForList[];
  pagination: Pagination;
  numberOfPages = 3;

  constructor(
    private notifyService: NotifyService,
    private announcementService: AnnouncementService,
    private route: ActivatedRoute,
    private location: Location,
    private adminService: AdminService
  ) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.announcements = data['announcements'].result;
      this.pagination = data['announcements'].pagination;
    });
  }

  goBack() {
    this.location.back();
  }

  loadAnnouncements() {
    this.announcementService.getMyAnnouncements(
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
