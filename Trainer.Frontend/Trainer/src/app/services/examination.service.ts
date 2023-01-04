import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Examination } from '../models/examination';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ExaminationService {
  private url = 'examination';

  constructor(private http: HttpClient) {}

  public getExaminations(): Observable<Examination[]> {
    return this.http.get<Examination[]>(`${environment.apiUrl}/${this.url}`);
  }

  public updateExamination(examination: Examination): Observable<Examination[]> {
    return this.http.put<Examination[]>(
      `${environment.apiUrl}/${this.url}`,
      examination
    );
  }

  public createExamination(examination: Examination): Observable<Examination[]> {
    return this.http.post<Examination[]>(
      `${environment.apiUrl}/${this.url}`,
      examination
    );
  }

  public deleteExamination(examination: Examination): Observable<Examination[]> {
    return this.http.delete<Examination[]>(
      `${environment.apiUrl}/${this.url}/${examination.id}`
    );
  }

  public donwload() {
    return this.http.get(
      `${environment.apiUrl}/${this.url}/export`,
      {observe: 'response', responseType: 'blob'}
    );
  }
}
