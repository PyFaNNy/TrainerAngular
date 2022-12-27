import {Component, OnInit} from "@angular/core";
import {Patient} from "../../../models/patient";
import {PatientService} from "../../../services/patient.service";
import {Examination} from "../../../models/examination";
import {ExaminationService} from "../../../services/examination.service";

@Component({
  selector: 'app-getExaminations',
  templateUrl: './getExaminations.component.html'
})

export class GetExaminationsComponent implements OnInit {
  examinations: Examination[] = [];
  examinationToEdit?: Examination;

  constructor(private examinationService: ExaminationService) {}

  ngOnInit(): void {
    this.examinationService
      .getExaminations()
      .subscribe((result: any) => ( this.examinations = result.Items));
  }
}
