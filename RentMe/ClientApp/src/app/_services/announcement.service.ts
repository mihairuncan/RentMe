import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Announcement } from '../_models/announcement';
import { Photo } from '../_models/photo';

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

  getPhotos(announcementId: string) {
    return this.http.get<Photo[]>(this.baseUrl + 'api/announcements/' + announcementId + '/photos');
  }

  setMainPhoto(announcementId: string, photoId: string) {
    return this.http.post(this.baseUrl + 'api/announcements/' + announcementId + '/photos/' + photoId + '/setMain', {});
  }

  deletePhoto(announcementId: string, photoId: string) {
    return this.http.delete(this.baseUrl + 'api/announcements/' + announcementId + '/photos/' + photoId);
  }
}
