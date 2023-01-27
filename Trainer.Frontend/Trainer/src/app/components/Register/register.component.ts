import {Component} from "@angular/core";
import {Router} from "@angular/router";
import {User} from "../../models/user";
import {UserService} from "../../services/user.service";
import {OtpService} from "../../services/otp.service";

@Component({
  selector: 'app-addPatient',
  templateUrl: './register.component.html'
})

export class RegisterComponent {
  showSpinner: boolean = false;
  user: User =new User;
  errors: any;

  constructor(private userService: UserService, private otpService: OtpService, private router: Router)
  {
    this.user.role = 'Doctor';
  }

  createUser() {
    this.showSpinner= true;
    this.userService
      .createUser(this.user)
      .subscribe(value => {
          this.router.navigate(['/verifycode',this.user.email,'Registration'])
        },
        error => {
          this.showSpinner= false;
          this.errors = error.error.errors
        });
  }
}
