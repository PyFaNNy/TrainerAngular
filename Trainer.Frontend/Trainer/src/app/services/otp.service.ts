import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import {User} from "../models/user";
import {Otp} from "../models/otp";

@Injectable({
  providedIn: 'root',
})
export class OtpService {
  private url = 'otp';

  constructor(private http: HttpClient) {}

  public verify(otp: Otp): Observable<boolean> {
    return this.http.post<boolean>(
      `${environment.apiUrl}/${this.url}/verify`,
      otp
    );
  }

  public resetPassowrdRequest(email: string): Observable<boolean> {
    const options = {
      headers: new HttpHeaders({
        'content-type' : 'application/json'
      })};
    return this.http.post<boolean>(
      `${environment.apiUrl}/${this.url}/reset?email=${email}`, null
    );
  }
}
