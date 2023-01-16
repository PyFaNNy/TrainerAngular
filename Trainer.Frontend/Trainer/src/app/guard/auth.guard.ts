import {CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router} from "@angular/router";
import {AuthService} from "../services/auth.service";
import {TokenService} from "../services/token.service";
import {Observable} from "rxjs/internal/Observable";
import {Injectable} from "@angular/core";

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate{

  constructor(private authService: AuthService, private tokenService: TokenService, private router: Router) {
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> {
    const url: string = state.url;

    return this.checkLogin(url);
  }

  checkLogin(url: string): any {
    if (this.tokenService.getRefreshToken()) {
      return true;
    }

    this.router.navigate(['/login']).then(_ => false);
  }
}
