import {Component} from "@angular/core";
import {PatientService} from "../../../services/patient.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-importPatients',
  templateUrl: './importPatients.component.html'
})

export class ImportPatientsComponent {
  file: any;
  errors: any;

  constructor(private patientService: PatientService, private router: Router) {
  }

  getFile(event: any) {
    this.file = event.target.files[0];
  }

  submitData() {
    let formData = new FormData();
    formData.set('name', 'patients');
    formData.set('file', this.file);

    this.patientService
      .load(formData)
      .subscribe(value => {
          this.router.navigate(['/patients'])
        },
        error => {
          this.errors = error.error.errors
        });
  }
}
