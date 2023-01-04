import {Component, OnInit} from "@angular/core";
import {User} from "../../models/user";
import {UserService} from "../../services/user.service";

@Component({
  selector: 'app-adminPanel',
  templateUrl: './admin-panel.component.html'
})

export class AdminPanelComponent  implements OnInit {
  users: User[] = [];
  userToEdit?: User;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.userService
      .getUsers()
      .subscribe((result: any) => ( this.users = result.items));
  }
}
