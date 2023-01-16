import { Injectable } from '@angular/core';
import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpResponse
} from "@angular/common/http";
import {Router} from "@angular/router";
import {AuthService} from "./auth.service";
import {TokenService} from "./token.service";
import {catchError, map, throwError} from "rxjs";

@Injectable({
  providedIn: 'root',
})
export class AuthInterceptor implements HttpInterceptor {
  constructor(private router: Router, private tokenService: TokenService, private authService: AuthService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): any {
    const token = this.tokenService.getToken();
    const refreshToken = this.tokenService.getRefreshToken();

    if (token) {
      request = request.clone({
        setHeaders: {
          Authorization: 'Bearer ' + token
        }
      });
    }

    request = request.clone({
      headers: request.headers.set('Accept', 'application/json')
    });

    return next.handle(request).pipe(
      map((event: HttpEvent<any>) => {
        return event;
      }),
      catchError((error: HttpErrorResponse) => {
        if (error.status === 401) {
          if (error.error.error === 'invalid_token') {
            this.authService.refreshToken({refresh_token: refreshToken})
              .subscribe(() => {
                location.reload();
              });
          } else {
            this.router.navigate(['login']);
          }
        }
        return throwError(error);
      }));
  }
}
