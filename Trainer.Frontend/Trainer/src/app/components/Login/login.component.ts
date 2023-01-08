import {Component} from "@angular/core";
import {User} from "../../models/user";
import {UserService} from "../../services/user.service";
import {Router} from "@angular/router";
import {AuthService} from "../../services/auth.service";

@Component({
  selector: 'app-addPatient',
  templateUrl: './login.component.html'
})

export class LoginComponent {
  user: User =new User;
  errors: any;

  constructor(private authService: AuthService, private router: Router)
  {
  }

  login() {
    this.authService
      .login(this.user)
      .subscribe(value => {
          this.router.navigate(['/verifycode',this.user.email,'Login'])
        },
        error => {
          this.errors = error.error
        });
  }
}
