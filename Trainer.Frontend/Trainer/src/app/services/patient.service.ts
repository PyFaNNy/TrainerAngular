import { HttpClient } from '@angular/common/http';
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

  public getPatients(): Observable<Patient[]> {
    return this.http.get<Patient[]>(`${environment.apiUrl}/${this.url}`);
  }

  public getPatient(id: string): Observable<Patient> {
    return this.http.get<Patient>(`${environment.apiUrl}/${this.url}/${id}`);
  }

  public updatePatient(patient: Patient): Observable<Patient[]> {
    return this.http.put<Patient[]>(
      `${environment.apiUrl}/${this.url}`,
      patient
    );
  }

  public createPatient(patient: Patient): Observable<Patient> {
    return this.http.post<Patient>(
      `${environment.apiUrl}/${this.url}`,
      patient
    );
  }

  public deletePatient(patient: Patient): Observable<Patient[]> {
    return this.http.delete<Patient[]>(
      `${environment.apiUrl}/${this.url}/${patient.id}`
    );
  }

  public donwload() {
    return this.http.get(
      `${environment.apiUrl}/${this.url}/exportcsv`,
      {observe: 'response', responseType: 'blob'}
    );
  }
}
