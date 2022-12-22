import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Examination } from '../models/examination';

@Injectable({
  providedIn: 'root',
})
export class SuperHeroService {
  private url = 'Examination';

  constructor(private http: HttpClient) {}

  public getSuperHeroes(): Observable<Examination[]> {
    return this.http.get<Examination[]>(`${environment.apiUrl}/${this.url}`);
  }

  public updatePatient(examination: Examination): Observable<Examination[]> {
    return this.http.put<Examination[]>(
      `${environment.apiUrl}/${this.url}`,
      examination
    );
  }

  public createPatient(examination: Examination): Observable<Examination[]> {
    return this.http.post<Examination[]>(
      `${environment.apiUrl}/${this.url}`,
      examination
    );
  }

  public deletePatient(examination: Examination): Observable<Examination[]> {
    return this.http.delete<Examination[]>(
      `${environment.apiUrl}/${this.url}/${examination.id}`
    );
  }
}
