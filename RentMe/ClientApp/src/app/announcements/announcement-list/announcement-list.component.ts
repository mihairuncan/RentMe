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
  searchText = '';

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
      this.pagination.itemsPerPage,
      this.searchText
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
    // window.scroll(0, 0);
    const scrollToTop = window.setInterval(() => {
      const pos = window.pageYOffset;
      if (pos > 0) {
        window.scrollTo(0, pos - 20); // how far to scroll on each step
      } else {
        window.clearInterval(scrollToTop);
      }
    }, 5);
  }

  search() {
    this.loadAnnouncements();
  }

  reset() {
    this.searchText = '';
    this.loadAnnouncements();
  }

}
