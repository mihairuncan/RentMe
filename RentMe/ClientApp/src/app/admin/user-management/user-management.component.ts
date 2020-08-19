import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/user';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { RolesModalComponent } from '../roles-modal/roles-modal.component';
import { NotifyService } from 'src/app/_services/notify.service';
import { AdminService } from 'src/app/_services/admin.service';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';
import { AuthenticationService } from 'src/app/_services/auth.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit {
  users: User[];
  pagination: Pagination;
  numberOfPages = 3;
  searchUserInput: string;
  bsModalRef: BsModalRef;

  constructor(
    private modalService: BsModalService,
    private notifyService: NotifyService,
    private adminService: AdminService,
    private route: ActivatedRoute,
  ) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.users = data['users'].result;
      this.pagination = data['users'].pagination;
    });
  }

  loadUsersWithRoles() {
    this.adminService.getUsersWithRoles(
      this.pagination.currentPage,
      this.pagination.itemsPerPage,
      this.searchUserInput
    )
      .subscribe((res: PaginatedResult<User[]>) => {
        this.users = res.result;
        this.pagination = res.pagination;
      }, error => {
        this.notifyService.error(error);
      });
  }

  editRolesModal(user: User) {
    const initialState = {
      user,
      roles: this.getRolesArray(user)
    };
    this.bsModalRef = this.modalService.show(RolesModalComponent, { initialState });
    this.bsModalRef.content.updateSelectedRoles.subscribe((values) => {
      const rolesToUpdate = {
        roleNames: [...values.filter(el => el.checked === true).map(el => el.name)]
      };
      if (rolesToUpdate) {
        this.adminService.updateUserRoles(user, rolesToUpdate).subscribe(() => {
          user.roles = [...rolesToUpdate.roleNames];
          this.notifyService.success('Roles successfully updated');
        }, error => {
          this.notifyService.error(error);
        });
      }
    });
  }

  private getRolesArray(user) {
    const roles = [];
    const userRoles = user.roles;
    const availableRoles: any[] = [
      { name: 'Admin', value: 'Admin' },
      { name: 'Moderator', value: 'Moderator' },
      { name: 'Regular', value: 'Regular' },
      { name: 'VIP', value: 'VIP' },
    ];

    for (let i = 0; i < availableRoles.length; i++) {
      let isMatch = false;
      for (let j = 0; j < userRoles.length; j++) {
        if (availableRoles[i].name === userRoles[j]) {
          isMatch = true;
          availableRoles[i].checked = true;
          roles.push(availableRoles[i]);
          break;
        }
      }
      if (!isMatch) {
        availableRoles[i].checked = false;
        roles.push(availableRoles[i]);
      }
    }
    return roles;
  }


  pageChange(pageNumber: number): void {
    this.pagination.currentPage = pageNumber;
    this.loadUsersWithRoles();
  }

  searchUser() {
    this.loadUsersWithRoles();
  }
}
