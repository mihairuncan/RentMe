import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';
import { AnnouncementForList } from 'src/app/_models/announcementForList';
import { NotifyService } from 'src/app/_services/notify.service';
import { AnnouncementService } from 'src/app/_services/announcement.service';
import { Location } from '@angular/common';

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
    private location: Location
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

}
