import {Component, OnInit} from "@angular/core";
import {PatientService} from "../../../services/patient.service";

@Component({
  selector: 'app-getPatients',
  styleUrls: ['./getPatients.component.scss'],
  templateUrl: './getPatients.component.html'

})

export class GetPatientsComponent implements OnInit {
  displayedColumns?: string[];
  patients: any[] = [];
  selectedPatient:any;
  isMasterSel:boolean = false;
  constructor(private patientService: PatientService) {}
  ngOnInit(): void {
    this.loadPatients();
    this.isMasterSel = false;
    this.displayedColumns = ['lastName', 'firstName', 'middleName', 'age', 'sex', 'isSelected', 'update', 'setExamination'];
  }

  downloadFile(): void {
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

  delete(): void
  {
    this.patientService
      .deletePatients(this.selectedPatient)
      .subscribe();
  }

  checkUncheckAll() {
    for (var i = 0; i < this.patients.length; i++) {
      this.patients[i].isSelected = this.isMasterSel;
    }
    this.getCheckedItemList();
  }

  isAllSelected() {
    this.isMasterSel = this.patients.every(function(item:any) {
      return item.isSelected == true;
    })
    this.getCheckedItemList();
  }

  getCheckedItemList(){
    this.selectedPatient = [];
    for (var i = 0; i < this.patients.length; i++) {
      if(this.patients[i].isSelected)
        this.selectedPatient.push(this.patients[i].id);
    }
    this.selectedPatient = JSON.stringify(this.selectedPatient);
  }

  private loadPatients()
  {
    this.patientService
      .getPatients()
      .subscribe((result: any) => {
        this.patients = result.items;
        this.patients = this.patients.map(x => (
          {
            id: x.id,
            isSelected: false,
            firstName: x.firstName,
            lastName: x.lastName,
            middleName: x.middleName,
            age: x.age,
            sex: x.sex
          }))
      });
  }
}
