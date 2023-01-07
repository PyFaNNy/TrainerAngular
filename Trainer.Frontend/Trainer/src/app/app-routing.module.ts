import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from "./components/Home/home.component";
import {ResetPasswordComponent} from "./components/ResetPassword/reset-password.component";
import {GetPatientsComponent} from "./components/Patients/GetPatients/getPatients.component";
import {AddPatientComponent} from "./components/Patients/AddPatient/addPatient.component";
import {ImportPatientsComponent} from "./components/Patients/ImportPatient/importPatients.component";
import {UpdatePatientComponent} from "./components/Patients/UpdatePatient/updatePatient.component";
import {GetExaminationsComponent} from "./components/Examinations/GetExaminations/getExaminations.component";
import {AddExaminationComponent} from "./components/Examinations/AddExamination/addExamination.component";
import {ImportExaminationsComponent} from "./components/Examinations/ImportExaminations/importExaminations.component";
import {AdminPanelComponent} from "./components/AdminPanel/admin-panel.component";
import {LoginComponent} from "./components/Login/login.component";
import {RegisterComponent} from "./components/Register/register.component";
import {ErrorComponent} from "./components/Error/error.component";

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
  imports: [RouterModule.forRoot(appRoute)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
