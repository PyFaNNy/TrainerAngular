import {Component, OnInit} from "@angular/core";
import {TokenService} from "../../services/token.service";
import {AuthService} from "../../services/auth.service";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html'
})

export class HeaderComponent implements OnInit {
  email: string = '';
  role: string = '';
  IsAuth: boolean = false;
  constructor(private tokenService: TokenService, private authService: AuthService) {
  }

  ngOnInit(): void {
    let token = this.tokenService.getToken();
    let decodeTok = this.tokenService.decodeToken(token);
    this.email = decodeTok.email;
    this.role = decodeTok.role;
    this.IsAuth = token != null;
  }

  logout()
  {
    this.authService
      .logout();
    window.location.replace("");
  }
}
