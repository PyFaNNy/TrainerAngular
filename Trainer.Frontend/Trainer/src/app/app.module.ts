import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";

import {HeaderComponent} from "./components/Header/header.component";
import {FooterComponent} from "./components/Footer/footer.component";
import {AddPatientComponent} from "./components/Patients/AddPatient/addPatient.component";
import {GetPatientComponent} from "./components/Patients/GetPatient/getPatient.component";
import {GetPatientsComponent} from "./components/Patients/GetPatients/getPatients.component";
import {UpdatePatientComponent} from "./components/Patients/UpdatePatient/updatePatient.component";
import {GetExaminationsComponent} from "./components/Examinations/GetExaminations/getExaminations.component";
import {AddExaminationComponent} from "./components/Examinations/AddExamination/addExamination.component";
import { HomeComponent } from './components/Home/home.component';
import {AdminPanelComponent} from "./components/AdminPanel/admin-panel.component";
import {RegisterComponent} from "./components/Register/register.component";
import {LoginComponent} from "./components/Login/login.component";
import {ResetPasswordComponent} from "./components/ResetPassword/reset-password.component";
import {ImportExaminationsComponent} from "./components/Examinations/ImportExaminations/importExaminations.component";
import {ImportPatientsComponent} from "./components/Patients/ImportPatient/importPatients.component";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {ErrorComponent} from "./components/Error/error.component";
import {MaterialModule} from "./material.module";
import {UpdateExaminationComponent} from "./components/Examinations/UpdateExamination/updateExamination.component";
import {GetExaminationComponent} from "./components/Examinations/GetExamination/getExamination.component";
import {VerifyCodeComponent} from "./components/VerifyCode/verify-code.component";
import {TuiModule} from "./tui.module";
import {AuthInterceptor} from "./services/auth.interceptor";
import {
  ExaminationDialogComponent
} from "./components/Examinations/GetExamination/ExaminationDialog/examination-dialog.component.";
import {SpinnerComponent} from "./components/Spinner/spinner.component";
import {SettingsComponent} from "./components/Settings/settings.component";

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    AddPatientComponent,
    GetPatientComponent,
    GetPatientsComponent,
    UpdatePatientComponent,
    GetExaminationsComponent,
    GetExaminationsComponent,
    AddExaminationComponent,
    HomeComponent,
    AdminPanelComponent,
    ErrorComponent,
    RegisterComponent,
    LoginComponent,
    ResetPasswordComponent,
    ImportExaminationsComponent,
    ImportPatientsComponent,
    UpdateExaminationComponent,
    GetExaminationComponent,
    VerifyCodeComponent,
    ExaminationDialogComponent,
    SpinnerComponent,
    SettingsComponent
  ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        NgbModule,
        HttpClientModule,
        FormsModule,
        BrowserAnimationsModule,
        MaterialModule,
        TuiModule,
        ReactiveFormsModule,
    ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
