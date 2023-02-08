import {Component} from "@angular/core";
import {Router} from "@angular/router";
import {User} from "../../models/user";
import {UserService} from "../../services/user.service";
import {OtpService} from "../../services/otp.service";
import {FormControl, FormGroup} from "@angular/forms";

@Component({
  selector: 'app-addPatient',
  templateUrl: './register.component.html'
})

export class RegisterComponent {
  showSpinner: boolean = false;
  registerForm: FormGroup;
  errors: any;

  constructor(private userService: UserService, private otpService: OtpService, private router: Router)
  {
    this._createForm();
  }

  submit() {
    this.showSpinner= true;
    this.userService
      .createUser(this.registerForm.value)
      .subscribe(value => {
          this.router.navigate(['/verifycode',this.registerForm.value.email,'Registration'])
        },
        error => {
          this.showSpinner= false;
          this.errors = error.error.errors
        });
  }

  private _createForm() {
    this.registerForm = new FormGroup({
      email: new FormControl(null),
      password: new FormControl(null),
      role: new FormControl(null),
      lastName: new FormControl(null),
      middleName: new FormControl(null),
      firstName: new FormControl(null),
      confirmPassword: new FormControl(null)
    })
  }
}
