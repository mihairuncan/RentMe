import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';
import { AnnouncementForList } from 'src/app/_models/announcementForList';
import { NotifyService } from 'src/app/_services/notify.service';
import { AnnouncementService } from 'src/app/_services/announcement.service';
import { Location } from '@angular/common';


@Component({
  selector: 'app-announcement-list',
  templateUrl: './announcement-list.component.html',
  styleUrls: ['./announcement-list.component.css']
})
export class AnnouncementListComponent implements OnInit {
  announcements: AnnouncementForList[];
  pagination: Pagination;
  numberOfPages = 3;
  subcategoryName: string;

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
    this.subcategoryName = this.route.snapshot.paramMap.get('subcategoryName');
  }

  goBack() {
    this.location.back();
  }

  loadAnnouncements() {
    this.announcementService.getAnnouncements(
      this.subcategoryName,
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
