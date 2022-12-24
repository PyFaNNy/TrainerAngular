import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {HttpClientModule} from "@angular/common/http";

import {HeaderComponent} from "./components/Header/header.component";
import {FooterComponent} from "./components/Footer/footer.component";
import {AddPatientComponent} from "./components/Patients/AddPatient/addPatient.component";
import {GetPatientComponent} from "./components/Patients/GetPatient/getPatient.component";
import {GetPatientsComponent} from "./components/Patients/GetPatients/getPatients.component";
import {UpdatePatientComponent} from "./components/Patients/UpdatePatient/updatePatient.component";
import {GetExaminationsComponent} from "./components/Examinations/GetExaminations/getExaminations.component";
import {AddExaminationComponent} from "./components/Examinations/AddExamination/addExamination.component";
import {Routes} from "@angular/router";
import { HomeComponent } from './components/Home/home.component';
import {AdminPanelComponent} from "./components/AdminPanel/admin-panel.component";

const appRoute: Routes  = [
  {path: 'home', component: HomeComponent},
  {path: 'patient', component: GetPatientsComponent},
  {path: 'examination', component: GetExaminationsComponent},
  {path: 'admin', component: GetExaminationsComponent}
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
    AdminPanelComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
