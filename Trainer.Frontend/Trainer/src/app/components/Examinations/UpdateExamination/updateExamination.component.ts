import {Component, OnDestroy, OnInit} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {Subscription} from "rxjs";
import {ExaminationService} from "../../../services/examination.service";
import {FormControl, FormGroup} from "@angular/forms";

@Component({
  selector: 'app-updateExamination',
  templateUrl: './updateExamination.component.html'
})

export class UpdateExaminationComponent implements OnInit, OnDestroy{
  examinationForm: FormGroup;
  errors: any = null;
  subscriptions: Subscription = new Subscription();

  constructor(private route: ActivatedRoute, private examinationService: ExaminationService, private router: Router) {
    this._createForm();
    this.subscriptions.add(route.params.subscribe(params => this.examinationForm.patchValue({id: params['id']})));
  }

  ngOnInit(): void {
    this.subscriptions.add(this.examinationService
      .getExamination(this.examinationForm.get('id')?.value)
      .subscribe((result: any) => {
        this.examinationForm.patchValue(
          {
            diaSis: result.diaSis,
            temperature: result.tempareture,
            heartRate: result.heartRate,
            spO2: result.sp02
          }
        )
      }));
  }

  private _createForm() {
    this.examinationForm = new FormGroup({
      typePhysicalActive: new FormControl(null),
      date: new FormControl(null),
      spO2: new FormControl(null),
      temperature: new FormControl(null),
      heartRate: new FormControl(null),
      diaSis: new FormControl(null),
      indicators: new FormControl(0),
      id: new FormControl()
    })
  }

  submit() {
    let ind =0;
    if (this.examinationForm.get('diaSis')) {
      ind +=1;
    }
    if (this.examinationForm.get('temperature')) {
      ind +=2;
    }
    if (this.examinationForm.get('heartRate')) {
      ind +=4;
    }
    if (this.examinationForm.get('spO2')) {
      ind +=8;
    }

    this.examinationService
      .updateExamination(this.examinationForm.value)
      .subscribe(value => {
          this.router.navigate(['/examinations'])
        },
        error => {
          this.errors = error.error.errors
        });
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }
}
