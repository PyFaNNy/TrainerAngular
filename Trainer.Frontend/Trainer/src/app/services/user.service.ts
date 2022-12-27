import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Patient } from '../models/patient';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private url = 'Patient';

  constructor(private http: HttpClient) {}

  public getUsers(): Observable<Patient[]> {
    return this.http.get<Patient[]>(`${environment.apiUrl}/${this.url}`);
  }

  public createPatient(patient: Patient): Observable<Patient[]> {
    return this.http.post<Patient[]>(
      `${environment.apiUrl}/${this.url}`,
      patient
    );
  }

  public deletePatient(patient: Patient): Observable<Patient[]> {
    return this.http.delete<Patient[]>(
      `${environment.apiUrl}/${this.url}/${patient.id}`
    );
  }
}
