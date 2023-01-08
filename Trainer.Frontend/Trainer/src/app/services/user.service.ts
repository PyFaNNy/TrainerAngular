import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import {User} from "../models/user";

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private url = 'baseUser';

  constructor(private http: HttpClient) {}

  public getUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${environment.apiUrl}/${this.url}`);
  }

  public createUser(user: User): Observable<User[]> {
    return this.http.post<User[]>(
      `${environment.apiUrl}/register`,
      user
    );
  }

  public deleteUsers(ids: string[]) {
    const options = {
      headers: new HttpHeaders({
        'accept': '*/*',
        'content-type' : 'application/json-patch+json'
      }),
      body: ids
    };
    return this.http.delete(
      `${environment.apiUrl}/${this.url}`,options)
  }

  public unblockUsers(ids: string[]) {
    const options = {
      headers: new HttpHeaders({
        'content-type' : 'application/json'
      })};
    return this.http.put(
      `${environment.apiUrl}/${this.url}/unblock`,ids,options)
  }

  public blockUsers(ids: string[]) {
    const options = {
      headers: new HttpHeaders({
        'content-type' : 'application/json'
      })};
    return this.http.put(
      `${environment.apiUrl}/${this.url}/block`,ids,options)
  }

  public approveUser(id: string) {
    return this.http.get(
      `${environment.apiUrl}/${this.url}/approve/${id}`)
  }

  public declineUser(id: string) {
    return this.http.get(
      `${environment.apiUrl}/${this.url}/decline/${id}`)
  }
}
