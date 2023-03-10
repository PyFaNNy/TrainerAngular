import {Component, OnInit} from "@angular/core";
import {UserService} from "../../services/user.service";
import {PageEvent} from "@angular/material/paginator";
import {Sort} from "@angular/material/sort";

@Component({
  selector: 'app-adminPanel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.scss'],
})

export class AdminPanelComponent implements OnInit {
  showSpinner: boolean = false;
  displayedColumns?: string[];
  users: any[] = [];
  selectedUsers: any;
  isMasterSel: boolean = false;
  length: number = 0;
  pageSize: number = 5;
  pageIndex: number = 0;
  pageEvent: PageEvent = new PageEvent();

  constructor(private userService: UserService) {
  }

  ngOnInit(): void {
    this.loadUsers();
    this.isMasterSel = false;
    this.displayedColumns = ['email', 'lastName', 'firstName', 'middleName', 'role', 'status', 'isSelected', 'btns'];
  }

  delete(): void {
    this.userService
      .deleteUsers(this.selectedUsers)
      .subscribe(() =>
        this.ngOnInit()
      );
  }

  block(): void {
    console.log(this.selectedUsers);
    this.userService
      .blockUsers(this.selectedUsers)
      .subscribe(() =>
        this.ngOnInit()
      );
  }

  unblock(): void {
    this.userService
      .unblockUsers(this.selectedUsers)
      .subscribe(() =>
        this.ngOnInit()
      );
  }

  approve(id: string): void {
    this.userService
      .approveUser(id)
      .subscribe(() =>
        this.ngOnInit()
      );
  }

  decline(id: string): void {
    this.userService
      .declineUser(id)
      .subscribe(() =>
        this.ngOnInit()
      );
  }

  checkUncheckAll() {
    for (var i = 0; i < this.users.length; i++) {
      this.users[i].isSelected = this.isMasterSel;
    }
    this.getCheckedItemList();
  }

  isAllSelected() {
    this.isMasterSel = this.users.every(function (item: any) {
      return item.isSelected == true;
    })
    this.getCheckedItemList();
  }

  getCheckedItemList() {
    this.selectedUsers = [];
    for (var i = 0; i < this.users.length; i++) {
      if (this.users[i].isSelected)
        this.selectedUsers.push(this.users[i].id);
    }
    this.selectedUsers = JSON.stringify(this.selectedUsers);
  }

  private loadUsers(sort?:any) {
    this.showSpinner = true;
    this.userService
      .getUsers(
        this.pageIndex+1 ?? 0,
        this.pageSize ?? 10,
        sort
      )
      .subscribe((result: any) => {
        this.pageIndex = result.pageIndex-1;
        this.length = result.totalCount;
        this.users = result.items;
        this.users = this.users.map(x => (
          {
            id: x.id,
            email: x.email,
            isSelected: false,
            firstName: x.firstName,
            lastName: x.lastName,
            middleName: x.middleName,
            role: x.role,
            status: x.status
          }))
        this.showSpinner = false;
      });
  }

  handlePageEvent(e: PageEvent) {
    this.pageEvent = e;
    this.length = e.length;
    this.pageSize = e.pageSize;
    this.pageIndex = e.pageIndex;

    this.loadUsers();
  }

  announceSortChange(sortState: Sort) {
    let sort;
    if (sortState.direction) {
      sort = sortState.active + "Sort";
      if (sortState.direction == "desc") {
        sort += "Desc";
      } else {
        sort += "Asc";
      }
    }
    this.loadUsers(sort);
  }
}
