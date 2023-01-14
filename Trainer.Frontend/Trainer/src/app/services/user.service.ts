import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import {User} from "../models/user";
import {Patient} from "../models/patient";

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private url = 'baseUser';

  constructor(private http: HttpClient) {}

  public getUsers(pageIndex:number, pageSize:number, sort:any): Observable<User[]> {
    if(sort !=null)
    {
      return this.http.get<User[]>(`${environment.apiUrl}/${this.url}?sortOrder=${sort}&pageIndex=${pageIndex}&pageSize=${pageSize}`);
    }
    else
    {
      return this.http.get<User[]>(`${environment.apiUrl}/${this.url}?pageIndex=${pageIndex}&pageSize=${pageSize}`);
    }
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
  public resetPasswordUser(user: User): Observable<User[]> {
    return this.http.post<User[]>(
      `${environment.apiUrl}/${this.url}/reset`,
      user
    );
  }
}
