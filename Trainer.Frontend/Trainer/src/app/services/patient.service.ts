import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Patient } from '../models/patient';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class PatientService {
  private url = 'patient';

  constructor(private http: HttpClient) {}

  public getPatients(pageIndex:number, pageSize:number, sort:any): Observable<Patient[]> {
    if(sort !=null)
    {
      return this.http.get<Patient[]>(`${environment.apiUrl}/${this.url}?sortOrder=${sort}&pageIndex=${pageIndex}&pageSize=${pageSize}`);
    }
    else
    {
      return this.http.get<Patient[]>(`${environment.apiUrl}/${this.url}?pageIndex=${pageIndex}&pageSize=${pageSize}`);
    }
  }

  public getPatient(id: string): Observable<Patient> {
    return this.http.get<Patient>(`${environment.apiUrl}/${this.url}/${id}`);
  }

  public updatePatient(patient: Patient): Observable<any> {
    return this.http.put<any>(
      `${environment.apiUrl}/${this.url}`,
      patient
    );
  }

  public createPatient(patient: Patient): Observable<any> {
    return this.http.post<any>(
      `${environment.apiUrl}/${this.url}`,
      patient
    );
  }

  public deletePatients(ids: string[]) {
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

  public donwload(): Observable<any> {
    return this.http.get(
      `${environment.apiUrl}/${this.url}/export`,
      {observe: 'response', responseType: 'blob'}
    );
  }

  public load(formData:FormData): Observable<any> {
    return this.http.post(
      `${environment.apiUrl}/${this.url}/import`,
      formData
    );
  }
}
