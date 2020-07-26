import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { User } from '../_models/user';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl: string;

  constructor(
    @Inject('BASE_URL') baseUrl: string,
    private http: HttpClient
  ) {
    this.baseUrl = baseUrl + 'api/admin/';
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

    return this.http.get<User[]>(this.baseUrl + 'usersWithRoles', { observe: 'response', params })
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
    return this.http.post(this.baseUrl + 'editRoles/' + user.userName, roles);
  }

  // getPhotosForApproval() {
  //   return this.http.get(this.baseUrl + 'admin/photosForModeration');
  // }

  // approvePhoto(photoId) {
  //   return this.http.post(this.baseUrl + 'admin/approvePhoto/' + photoId, {});
  // }

  // rejectPhoto(photoId) {
  //   return this.http.post(this.baseUrl + 'admin/rejectPhoto/' + photoId, {});
  // }

}
