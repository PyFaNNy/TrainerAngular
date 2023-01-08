import {Component} from "@angular/core";
import {Patient} from "../../models/patient";
import {Subscription} from "rxjs";
import {ActivatedRoute, Router} from "@angular/router";
import {PatientService} from "../../services/patient.service";
import {User} from "../../models/user";
import {UserService} from "../../services/user.service";
import {OtpService} from "../../services/otp.Service";

@Component({
  selector: 'app-addPatient',
  templateUrl: './register.component.html'
})

export class RegisterComponent {
  user: User =new User;
  errors: any;

  constructor(private userService: UserService, private otpService: OtpService, private router: Router)
  {
    this.user.role = 'Doctor';
  }

  createUser() {
    this.userService
      .createUser(this.user)
      .subscribe(value => {
          this.router.navigate(['/verifycode',this.user.email,'Registration'])
        },
        error => {
          this.errors = error.error.errors
        });
  }
}
