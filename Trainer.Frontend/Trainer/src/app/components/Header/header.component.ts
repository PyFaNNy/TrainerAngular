import {Component, OnInit} from "@angular/core";
import {TokenService} from "../../services/token.service";
import {AuthService} from "../../services/auth.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html'
})

export class HeaderComponent implements OnInit {
  email: string = '';
  IsAuth: boolean = false;
  constructor(private tokenService: TokenService, private authService: AuthService, private router: Router) {
  }

  ngOnInit(): void {
    let token = this.tokenService.getToken();
    this.IsAuth = token != null;
  }

  logout()
  {
    this.authService
      .logout();
    this.ngOnInit();
    this.router.navigate(['/'])
  }
}
