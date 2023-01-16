import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TokenService {
  private ACCESS_TOKEN = 'access_token';
  private REFRESH_TOKEN = 'refresh_token';
  constructor() { }
  getToken(): any {
    return localStorage.getItem(this.ACCESS_TOKEN);
  }

  getRefreshToken(): any {
    return localStorage.getItem(this.REFRESH_TOKEN);
  }

  saveToken(token:any): void {
    localStorage.setItem(this.ACCESS_TOKEN, token);
  }

  saveRefreshToken(refreshToken:any): void {
    localStorage.setItem(this.REFRESH_TOKEN, refreshToken);
  }

  removeToken(): void {
    localStorage.removeItem(this.ACCESS_TOKEN);
  }

  removeRefreshToken(): void {
    localStorage.removeItem(this.REFRESH_TOKEN);
  }
}
