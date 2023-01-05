import {Component} from "@angular/core";
import {Patient} from "../../../models/patient";
import {PatientService} from "../../../services/patient.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-addPatient',
  templateUrl: './addPatient.component.html'
})

export class AddPatientComponent {
  patient: Patient =new Patient;
  errors: any;

  constructor(private patientsService: PatientService, private router: Router) {}

  createPatient(patient: Patient) {
    this.patientsService
      .createPatient(patient)
      .subscribe(value => {
          this.router.navigate(['/patients'])
        },
      error => {
        this.errors = error.error.errors
      });
  }
}
