import {Component, OnInit} from "@angular/core";
import {Patient} from "../../../models/patient";
import {PatientService} from "../../../services/patient.service";

@Component({
  selector: 'app-getPatients',
  templateUrl: './getPatients.component.html'
})

export class GetPatientsComponent implements OnInit {
  patients: Patient[] = [];

  constructor(private patientService: PatientService) {}

  ngOnInit(): void {
    this.patientService
      .getPatients()
      .subscribe((result: any) => ( this.patients = result.items));
  }

  public downloadFile(): void {
    this.patientService
      .donwload()
      .subscribe(response => {
        let fileName = 'patients';
        let blob: Blob = response.body as Blob;
        let a = document.createElement('a');
        a.download = fileName;
        a.href = window.URL.createObjectURL(blob);
        a.click();
      });
  }
}
