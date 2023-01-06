import {Component, OnInit} from "@angular/core";
import {User} from "../../models/user";
import {UserService} from "../../services/user.service";

@Component({
  selector: 'app-adminPanel',
  templateUrl: './admin-panel.component.html'
})

export class AdminPanelComponent implements OnInit {
  users: any[] = [];
  selectedUsers: any;
  isMasterSel: boolean = false;

  constructor(private userService: UserService) {
  }

  ngOnInit(): void {
    this.loadUsers();
    this.isMasterSel = false;
  }

  delete(): void {
    this.userService
      .deleteUsers(this.selectedUsers)
      .subscribe(result =>
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

  private loadUsers() {
    this.userService
      .getUsers()
      .subscribe((result: any) => {
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
      });
  }
}
