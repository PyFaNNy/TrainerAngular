import {Component, OnInit} from "@angular/core";
import {ExaminationService} from "../../../services/examination.service";
import {PageEvent} from "@angular/material/paginator";

@Component({
  selector: 'app-getExaminations',
  templateUrl: './getExaminations.component.html',
  styleUrls: ['./getExaminations.component.scss'],
})

export class GetExaminationsComponent implements OnInit {
  displayedColumns?: string[];
  examinations: any[] = [];
  selectedExaminations: any;
  isMasterSel: boolean = false;
  length: number = 0;
  pageSize: number = 5;
  pageIndex: number = 0;
  pageEvent: PageEvent = new PageEvent();

  constructor(private examinationService: ExaminationService) {
  }

  ngOnInit(): void {
    this.loadExamination();
    this.isMasterSel = false;
    this.displayedColumns = ['typePhysicalActive', 'lastName', 'firstName', 'middleName', 'date', 'isSelected', 'btns'];
  }

  downloadFile(): void {
    this.examinationService
      .donwload()
      .subscribe(response => {
        let fileName = 'examination';
        let blob: Blob = response.body as Blob;
        let a = document.createElement('a');
        a.download = fileName;
        a.href = window.URL.createObjectURL(blob);
        a.click();
      });
  }

  delete(): void {
    this.examinationService
      .deleteExaminations(this.selectedExaminations)
      .subscribe(() =>
        this.ngOnInit()
      );
  }

  checkUncheckAll() {
    for (var i = 0; i < this.examinations.length; i++) {
      this.examinations[i].isSelected = this.isMasterSel;
    }
    this.getCheckedItemList();
  }

  isAllSelected() {
    this.isMasterSel = this.examinations.every(function (item: any) {
      return item.isSelected == true;
    })
    this.getCheckedItemList();
  }

  getCheckedItemList() {
    this.selectedExaminations = [];
    for (var i = 0; i < this.examinations.length; i++) {
      if (this.examinations[i].isSelected)
        this.selectedExaminations.push(this.examinations[i].id);
    }
    this.selectedExaminations = JSON.stringify(this.selectedExaminations);
  }

  private loadExamination() {
    this.examinationService
      .getExaminations(
        this.pageIndex+1 ?? 0,
        this.pageSize ?? 10
      )
      .subscribe((result: any) => {
        this.pageIndex = result.pageIndex-1;
        this.length = result.totalCount;
        this.examinations = result.items;
        this.examinations = this.examinations.map(x => (
          {
            id: x.id,
            isSelected: false,
            patient: {
              firstName: x.patient.firstName,
              lastName: x.patient.lastName,
              middleName: x.patient.middleName,
            },
            typePhysicalActive: x.typePhysicalActive,
            date: x.date
          }))
      });
  }

  handlePageEvent(e: PageEvent) {
    this.pageEvent = e;
    this.length = e.length;
    this.pageSize = e.pageSize;
    this.pageIndex = e.pageIndex;

    this.loadExamination();
  }
}
