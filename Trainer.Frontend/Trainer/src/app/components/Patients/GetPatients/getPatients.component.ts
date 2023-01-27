import {Component, OnInit} from "@angular/core";
import {PatientService} from "../../../services/patient.service";
import {PageEvent} from "@angular/material/paginator";
import {Sort} from "@angular/material/sort";
import {TokenService} from "../../../services/token.service";

@Component({
  selector: 'app-getPatients',
  styleUrls: ['./getPatients.component.scss'],
  templateUrl: './getPatients.component.html'

})

export class GetPatientsComponent implements OnInit {
  showSpinner: boolean = false;
  displayedColumns?: string[];
  patients: any[] = [];
  selectedPatient: any;
  isMasterSel: boolean = false;
  length: number = 0;
  pageSize: number = 5;
  pageIndex: number = 0;
  pageEvent: PageEvent = new PageEvent();
  role:string ="";
  constructor(private patientService: PatientService, private tokenService: TokenService) {
  }

  ngOnInit(): void {
    this.loadPatients();
    this.isMasterSel = false;
    this.displayedColumns = ['lastName', 'firstName', 'middleName', 'age', 'sex', 'isSelected', 'setExamination'];
    this.role = this.tokenService.decodeToken(this.tokenService.getToken()).role;
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

  delete(): void {
    this.patientService
      .deletePatients(this.selectedPatient)
      .subscribe();
  }

  checkUncheckAll() {
    for (let i = 0; i < this.patients.length; i++) {
      this.patients[i].isSelected = this.isMasterSel;
    }
    this.getCheckedItemList();
  }

  isAllSelected() {
    this.isMasterSel = this.patients.every(function (item: any) {
      return item.isSelected == true;
    })
    this.getCheckedItemList();
  }

  getCheckedItemList() {
    this.selectedPatient = [];
    for (let i = 0; i < this.patients.length; i++) {
      if (this.patients[i].isSelected)
        this.selectedPatient.push(this.patients[i].id);
    }
    this.selectedPatient = JSON.stringify(this.selectedPatient);
  }

  private loadPatients(sort?:any) {
    this.showSpinner= true;
    this.patientService
      .getPatients(
        this.pageIndex + 1 ?? 0,
        this.pageSize ?? 10,
        sort
      )
      .subscribe((result: any) => {
        this.pageIndex = result.pageIndex - 1;
        this.length = result.totalCount;
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
        this.showSpinner= false;
      });
  }

  handlePageEvent(e: PageEvent) {
    this.pageEvent = e;
    this.length = e.length;
    this.pageSize = e.pageSize;
    this.pageIndex = e.pageIndex;

    this.loadPatients();
  }

  announceSortChange(sortState: Sort) {
    let sort;
    if (sortState.direction) {
      sort = sortState.active + "Sort";
      if (sortState.direction == "desc") {
        sort += "Desc";
      } else {
        sort += "Asc";
      }
    }
    this.loadPatients(sort);
  }
}
