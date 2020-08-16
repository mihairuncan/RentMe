import { Component, OnInit, Input, AfterContentInit } from '@angular/core';
import { AnnouncementForList } from 'src/app/_models/announcementForList';
import { Output, EventEmitter } from '@angular/core';


@Component({
  selector: 'app-announcement-card',
  templateUrl: './announcement-card.component.html',
  styleUrls: ['./announcement-card.component.css']
})
export class AnnouncementCardComponent implements OnInit, AfterContentInit {
  @Input() announcement: AnnouncementForList;
  @Output() rejectAnnouncementEvent = new EventEmitter<string>();
  @Output() approveAnnouncementEvent = new EventEmitter<string>();

  constructor() { }

  ngOnInit() {
    if (this.announcement.mainPhotoUrl === null) {
      this.announcement.mainPhotoUrl = '../../assets/images/no-image.jpg';
    }
  }

  ngAfterContentInit(): void {
    document.querySelector('.mat-card-header-text').remove();
  }

  rejectAnnouncement(announcementId: string) {
    this.rejectAnnouncementEvent.emit(announcementId);
  }

  approveAnnouncement(announcementId: string) {
    this.approveAnnouncementEvent.emit(announcementId);
  }


}
