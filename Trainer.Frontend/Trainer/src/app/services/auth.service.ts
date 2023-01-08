import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import {User} from "../models/user";

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private url = 'auth';

  constructor(private http: HttpClient) {}

  public login(user: User): Observable<User[]> {
    return this.http.post<User[]>(
      `${environment.apiUrl}/login`,
      user
    );
  }
}
