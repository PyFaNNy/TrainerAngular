import {Component} from "@angular/core";
import {Subscription} from "rxjs";
import {ActivatedRoute, Router} from "@angular/router";
import {User} from "../../models/user";
import {UserService} from "../../services/user.service";

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss'],
})

export class SettingsComponent {
  user: User = new User;
  errors: any;
  subscriptions: Subscription = new Subscription();

  constructor(private route: ActivatedRoute, private userService: UserService,private router: Router){
    this.subscriptions.add(route.params.subscribe(params=>this.user.email=params['email']));
  }

  reset()
  {
    this.subscriptions.add(this.userService
      .resetPasswordUser(this.user)
      .subscribe(
        result  => {
          this.router.navigate(['/login'])
        },
        error => {
          this.errors = error.error
        }));
  };
}
