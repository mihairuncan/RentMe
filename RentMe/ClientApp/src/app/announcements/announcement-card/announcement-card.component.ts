import { Component, OnInit, Input } from '@angular/core';
import { AnnouncementForList } from 'src/app/_models/announcementForList';
import { Output, EventEmitter } from '@angular/core';


@Component({
  selector: 'app-announcement-card',
  templateUrl: './announcement-card.component.html',
  styleUrls: ['./announcement-card.component.css']
})
export class AnnouncementCardComponent implements OnInit {
  @Input() announcement: AnnouncementForList;
  @Output() rejectAnnouncementEvent = new EventEmitter<string>();
  @Output() approveAnnouncementEvent = new EventEmitter<string>();

  constructor() { }

  ngOnInit() {
  }

  rejectAnnouncement(announcementId: string) {
    this.rejectAnnouncementEvent.emit(announcementId);
  }

  approveAnnouncement(announcementId: string) {
    this.approveAnnouncementEvent.emit(announcementId);
  }
}
