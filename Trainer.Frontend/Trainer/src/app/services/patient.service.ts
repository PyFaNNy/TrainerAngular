import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Patient } from '../models/patient';

@Injectable({
  providedIn: 'root',
})
export class SuperHeroService {
  private url = 'Patient';

  constructor(private http: HttpClient) {}

  public getSuperHeroes(): Observable<Patient[]> {
    return this.http.get<Patient[]>(`${environment.apiUrl}/${this.url}`);
  }

  public updatePatient(patient: Patient): Observable<Patient[]> {
    return this.http.put<Patient[]>(
      `${environment.apiUrl}/${this.url}`,
      patient
    );
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
