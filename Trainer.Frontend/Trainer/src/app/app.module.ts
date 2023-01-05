import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {HttpClientModule} from "@angular/common/http";
import {RouterModule, Routes} from "@angular/router";

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
import { ErrorComponent } from './components/error/error.component';
import {RegisterComponent} from "./components/Register/register.component";
import {LoginComponent} from "./components/Login/login.component";
import {ResetPasswordComponent} from "./components/ResetPassword/reset-password.component";
import {FormsModule} from "@angular/forms";
import {ImportExaminationsComponent} from "./components/Examinations/ImportExaminations/importExaminations.component";
import {ImportPatientsComponent} from "./components/Patients/ImportPatient/importPatients.component";

const appRoute: Routes  = [
  {path: '', redirectTo: 'home', pathMatch: 'full'},
  {path: 'home', component: HomeComponent},
  {path: 'reset', component: ResetPasswordComponent},
  {path: 'patients', component: GetPatientsComponent},
  {path: 'addPatient', component: AddPatientComponent},
  {path: 'importPatients', component: ImportPatientsComponent},
  {path: 'updatePatient/:id', component: UpdatePatientComponent},
  {path: 'examinations', component: GetExaminationsComponent},
  {path: 'addExamination/:id', component: AddExaminationComponent},
  {path: 'importExaminations', component: ImportExaminationsComponent},
  {path: 'admin', component: AdminPanelComponent},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: '**', component: ErrorComponent},
]

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
    ImportPatientsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    HttpClientModule,
    RouterModule.forRoot(appRoute),
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
