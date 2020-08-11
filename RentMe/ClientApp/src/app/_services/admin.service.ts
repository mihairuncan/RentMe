import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { User } from '../_models/user';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';
import { AnnouncementForList } from '../_models/announcementForList';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl: string;

  constructor(
    @Inject('BASE_URL') baseUrl: string,
    private http: HttpClient
  ) {
    this.baseUrl = baseUrl;
  }

  getUsersWithRoles(page?, itemsPerPage?, searchUserInput?) {

    const paginatedResult: PaginatedResult<User[]> = new PaginatedResult<User[]>();
    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    if (searchUserInput != null && searchUserInput !== '') {
      params = params.append('username', searchUserInput);
    }

    return this.http.get<User[]>(this.baseUrl + 'api/admin/usersWithRoles', { observe: 'response', params })
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

  updateUserRoles(user: User, roles: {}) {
    return this.http.post(this.baseUrl + 'api/admin/editRoles/' + user.userName, roles);
  }

  getUnapprovedAnnouncements(page?, itemsPerPage?) {
    const paginatedResult: PaginatedResult<AnnouncementForList[]> = new PaginatedResult<AnnouncementForList[]>();
    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<AnnouncementForList[]>(this.baseUrl + 'api/announcements/unapproved', { observe: 'response', params })
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

  approveAnnouncement(announcementId: string) {
    return this.http.put(this.baseUrl + 'api/announcements/' + announcementId + '/approve', {});
  }

  rejectAnnouncement(announcementId: string) {
    return this.http.delete(this.baseUrl + 'api/announcements/' + announcementId + '/reject');
  }
}
