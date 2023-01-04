import {Component} from "@angular/core";
import {Patient} from "../../../models/patient";
import {PatientService} from "../../../services/patient.service";

@Component({
  selector: 'app-addPatient',
  templateUrl: './addPatient.component.html'
})

export class AddPatientComponent {
  patient: Patient =new Patient;
  result: any;

  constructor(private patientsService: PatientService) {}

  createPatient(patient: Patient) {
    this.patientsService
      .createPatient(patient)
      .subscribe((result: any) => ( this.result = result));
  }
}
