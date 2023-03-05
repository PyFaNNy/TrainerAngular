import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs/internal/Observable";
import {environment} from "../../environments/environment";
import {UsersError} from "../models/Errors/UsersError";
import {CSVError} from "../models/Errors/CSVError";
import {PatientError} from "../models/Errors/PatientError";
import {ExaminationError} from "../models/Errors/ExaminationError";
import {ResultError} from "../models/Errors/ResultError";
import {OTPError} from "../models/Errors/OTPError";

@Injectable({
  providedIn: 'root',
})
export class SettingsService {
  private url = 'config';

  constructor(private http: HttpClient) {}

  public getUsersErrorSettings(): Observable<UsersError> {
    return this.http.get<UsersError>(`${environment.apiUrl}/${this.url}/users`);
  }

  public updateUsersErrorSettings(errors: UsersError): Observable<UsersError> {
    return this.http.put<UsersError>(
      `${environment.apiUrl}/${this.url}/users`,
      errors
    );
  }

  public getCSVErrorSettings(): Observable<CSVError> {
    return this.http.get<CSVError>(`${environment.apiUrl}/${this.url}/csv`);
  }

  public updateCSVErrorSettings(errors: CSVError): Observable<CSVError> {
    return this.http.put<CSVError>(
      `${environment.apiUrl}/${this.url}/csv`,
      errors
    );
  }

  public getPatientsErrorSettings(): Observable<PatientError> {
    return this.http.get<PatientError>(`${environment.apiUrl}/${this.url}/patient`);
  }

  public updatePatientsErrorSettings(errors: PatientError): Observable<PatientError> {
    return this.http.put<PatientError>(
      `${environment.apiUrl}/${this.url}/patient`,
      errors
    );
  }

  public getExaminationsErrorSettings(): Observable<ExaminationError> {
    return this.http.get<ExaminationError>(`${environment.apiUrl}/${this.url}/examination`);
  }

  public updateExaminationsErrorSettings(errors: ExaminationError): Observable<ExaminationError> {
    return this.http.put<ExaminationError>(
      `${environment.apiUrl}/${this.url}/examination`,
      errors
    );
  }

  public getResultsErrorSettings(): Observable<ResultError> {
    return this.http.get<ResultError>(`${environment.apiUrl}/${this.url}/result`);
  }

  public updateResultsErrorSettings(errors: ResultError): Observable<ResultError> {
    return this.http.put<ResultError>(
      `${environment.apiUrl}/${this.url}/result`,
      errors
    );
  }

  public getOTPErrorSettings(): Observable<OTPError> {
    return this.http.get<OTPError>(`${environment.apiUrl}/${this.url}/otp`);
  }

  public updateOTPErrorSettings(errors: OTPError): Observable<OTPError> {
    return this.http.put<OTPError>(
      `${environment.apiUrl}/${this.url}/otp`,
      errors
    );
  }
}
