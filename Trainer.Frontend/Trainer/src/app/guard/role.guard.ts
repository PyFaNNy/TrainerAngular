import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from "@angular/router";
import {AuthService} from "../services/auth.service";
import {TokenService} from "../services/token.service";
import {Observable} from "rxjs/internal/Observable";

@Injectable({
  providedIn: 'root',
})
export class RoleGuard implements CanActivate{

  constructor(private authService: AuthService, private tokenService: TokenService, private router: Router) {
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> {

    return this.checkRole(next);
  }

  checkRole(route: ActivatedRouteSnapshot): any {
    const userRole = this.tokenService.decodeToken(this.tokenService.getToken()).role;
    if (route.data['role'] && route.data['role'].indexOf(userRole) === -1) {
      this.router.navigate(['/home']);
      return false;
    }
    return true;

    this.router.navigate(['/error']).then(_ => false);
  }
}
