import {Component} from "@angular/core";
import {PatientService} from "../../../services/patient.service";
import {Router} from "@angular/router";
import {FormControl, FormGroup} from "@angular/forms";

@Component({
  selector: 'app-addPatient',
  templateUrl: './addPatient.component.html'
})

export class AddPatientComponent {
  patientForm: FormGroup;
  showSpinner: boolean = false;
  errors: any;

  constructor(private patientsService: PatientService, private router: Router) {
    this._createForm();
  }

  private _createForm() {
    this.patientForm = new FormGroup({
      firstName: new FormControl(null),
      lastName: new FormControl(null),
      middleName: new FormControl(null),
      email: new FormControl(null),
      age: new FormControl(null),
      sex: new FormControl(null),
    })
  }

  submit(){
    this.showSpinner = true;
    this.patientsService
      .createPatient(this.patientForm.value)
      .subscribe(value => {
          this.showSpinner = false;
          this.router.navigate(['/patients'])
        },
        error => {
          this.showSpinner = false;
          this.errors = error.error.errors
        });
  }
}
