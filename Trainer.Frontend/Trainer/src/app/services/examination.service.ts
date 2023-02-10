import {HttpClient, HttpHeaders} from '@angular/common/http';
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

  public getExaminations(pageIndex:number, pageSize:number, sort:any): Observable<Examination[]> {
    if(sort !=null)
    {
      return this.http.get<Examination[]>(`${environment.apiUrl}/${this.url}?sortOrder=${sort}&pageIndex=${pageIndex}&pageSize=${pageSize}`);
    }
    else
    {
      return this.http.get<Examination[]>(`${environment.apiUrl}/${this.url}?pageIndex=${pageIndex}&pageSize=${pageSize}`);
    }
  }

  public getExamination(id: string): Observable<Examination> {
    return this.http.get<Examination>(`${environment.apiUrl}/${this.url}/${id}`);
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

  public deleteExaminations(ids: string[]) {
    const options = {
      headers: new HttpHeaders({
        'accept': '*/*',
        'content-type' : 'application/json-patch+json'
      }),
      body: ids
    };
    return this.http.delete(
      `${environment.apiUrl}/${this.url}`,options
    );
  }

  public donwload() {
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
