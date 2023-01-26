import {HttpClient, HttpErrorResponse, HttpHeaders, HttpParams} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import {User} from "../models/user";
import {catchError, tap, throwError} from "rxjs";
import {TokenService} from "./token.service";

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private url = 'auth';
  private OAUTH_CLIENT = 'angular_client';
  private OAUTH_SECRET = 'angular_client';
  private API_URL = 'https://localhost:10001/connect/token';
  private HTTP_OPTIONS = {
    headers: new HttpHeaders({
      'Content-Type': 'application/x-www-form-urlencoded',
      Authorization: 'Basic ' + btoa(this.OAUTH_CLIENT + ':' + this.OAUTH_SECRET)
    })
  };

  private static handleError(error: HttpErrorResponse): any {
    return error;
  }

  constructor(private http: HttpClient, private tokenService: TokenService) {
  }

  login(loginData: any): Observable<any> {
    this.tokenService.removeToken();
    this.tokenService.removeRefreshToken();
    const body = new HttpParams()
      .set('username', loginData.email)
      .set('password', loginData.password)
      .set('grant_type', 'password');

    return this.http.post<any>(this.API_URL, body, this.HTTP_OPTIONS)
      .pipe(
        tap(res => {
          this.tokenService.saveToken(res.access_token);
          this.tokenService.saveRefreshToken(res.refresh_token);
        })
      );
  }

  refreshToken(refreshData: any): Observable<any> {
    this.tokenService.removeToken();
    this.tokenService.removeRefreshToken();
    const body = new HttpParams()
      .set('refresh_token', refreshData.refresh_token)
      .set('grant_type', 'refresh_token');
    return this.http.post<any>(this.API_URL, body, this.HTTP_OPTIONS)
      .pipe(
        tap(res => {
          this.tokenService.saveToken(res.access_token);
          this.tokenService.saveRefreshToken(res.refresh_token);
        })
      );
  }

  logout(): void {
    this.tokenService.removeToken();
    this.tokenService.removeRefreshToken();
  }
}
