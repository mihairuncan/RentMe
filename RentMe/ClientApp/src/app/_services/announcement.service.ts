import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Announcement } from '../_models/announcement';
import { Photo } from '../_models/photo';
import { PaginatedResult } from '../_models/pagination';
import { AnnouncementForList } from '../_models/announcementForList';
import { map } from 'rxjs/operators';

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
    return this.http.post(this.baseUrl + 'api/announcements', announcement);
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

  getAnnouncements(subcategoryName: string, page?, itemsPerPage?) {
    const paginatedResult: PaginatedResult<AnnouncementForList[]> = new PaginatedResult<AnnouncementForList[]>();
    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<AnnouncementForList[]>(this.baseUrl + 'api/announcements/subcategory/'
      + subcategoryName, { observe: 'response', params })
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          return paginatedResult;
        })
      );
  }
}
