import {Component, OnInit} from "@angular/core";
import {Examination} from "../../../models/examination";
import {ExaminationService} from "../../../services/examination.service";

@Component({
  selector: 'app-getExaminations',
  templateUrl: './getExaminations.component.html'
})

export class GetExaminationsComponent implements OnInit {
  examinations: Examination[] = [];

  constructor(private examinationService: ExaminationService) {}

  ngOnInit(): void {
    this.examinationService
      .getExaminations()
      .subscribe((result: any) => ( this.examinations = result.items));
  }

  public downloadFile(): void {
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
}
