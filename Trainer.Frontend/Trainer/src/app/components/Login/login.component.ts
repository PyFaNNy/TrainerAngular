import {Component} from "@angular/core";
import {UserService} from "../../services/user.service";
import {Router} from "@angular/router";
import {AuthService} from "../../services/auth.service";
import {OtpService} from "../../services/otp.service";
import {Otp} from "../../models/otp";
import {FormControl, FormGroup} from "@angular/forms";

@Component({
  selector: 'app-addPatient',
  templateUrl: './login.component.html'
})

export class LoginComponent {
  showSpinner: boolean = false;
  loginForm: FormGroup;
  errors: any;

  constructor(private authService: AuthService, private otpService: OtpService, private router: Router) {
    this._createForm();
  }

  private _createForm() {
    this.loginForm = new FormGroup({
      email: new FormControl(null),
      password: new FormControl(null)
    })
  }

  submit()
  {
    this.showSpinner = true;
    this.authService
      .login(this.loginForm.value)
      .subscribe(value => {
          this.showSpinner = false;
          window.location.replace("");
          // this.router.navigate(['/verifycode',this.user.email,'Login'])
        },
        error => {
            this.showSpinner = false;
            this.errors = "Login/Password incorrect"
        });
  }

  reset() {
    this.otpService
      .resetPassowrdRequest(this.loginForm.get('email')?.value)
      .subscribe(value => {
          this.router.navigate(['/verifycode', this.loginForm.get('email')?.value, 'ResetPassword'])
        },
        error => {
          this.errors = error.error
        });
  }
}
