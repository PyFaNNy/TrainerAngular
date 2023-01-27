import {Component} from "@angular/core";
import {User} from "../../models/user";
import {UserService} from "../../services/user.service";
import {Router} from "@angular/router";
import {AuthService} from "../../services/auth.service";
import {OtpService} from "../../services/otp.service";
import {Otp} from "../../models/otp";

@Component({
  selector: 'app-addPatient',
  templateUrl: './login.component.html'
})

export class LoginComponent {
  showSpinner: boolean = false;
  user: User = new User;
  errors: any;

  constructor(private authService: AuthService, private otpService: OtpService, private router: Router) {
  }

  login() {
    this.showSpinner = true;
    this.authService
      .login(this.user)
      .subscribe(value => {
          this.showSpinner = false;
          window.location.replace("");
          // this.router.navigate(['/verifycode',this.user.email,'Login'])
        },
        error => {
          if (error.status < 500) {
            this.showSpinner = false;
            this.errors = "Login/Password incorrect"
          }
        });
  }

  reset() {
    this.otpService
      .resetPassowrdRequest(this.user.email)
      .subscribe(value => {
          this.router.navigate(['/verifycode', this.user.email, 'ResetPassword'])
        },
        error => {
          this.errors = error.error
        });
  }
}
