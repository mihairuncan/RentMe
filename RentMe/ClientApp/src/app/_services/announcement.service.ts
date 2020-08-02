import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Announcement } from '../_models/announcement';

@Injectable({
  providedIn: 'root'
})
export class AnnouncementService {
  baseUrl: string;


  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.baseUrl = baseUrl;
  }


  add(announcement: Announcement) {
    return this.http.post(this.baseUrl + 'api/announcements/new', announcement);
  }
}
