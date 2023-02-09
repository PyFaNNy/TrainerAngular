import {Component} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {Subscription} from "rxjs";
import {ExaminationService} from "../../../services/examination.service";
import {FormControl, FormGroup} from "@angular/forms";

@Component({
  selector: 'app-addExamination',
  templateUrl: './addExamination.component.html'
})

export class AddExaminationComponent {
  showSpinner: boolean;
  examinationForm: FormGroup;
  errors: any = null;
  subscriptions: Subscription = new Subscription();
  constructor(private route: ActivatedRoute, private examinationService: ExaminationService,private router: Router){
    this._createForm();
    this.subscriptions.add(route.params.subscribe(params=>this.examinationForm.patchValue({patientId: params['id']})));
  }
  private _createForm() {
    this.examinationForm = new FormGroup({
      typePhysicalActive: new FormControl(null),
      date: new FormControl(null),
      spO2: new FormControl(null),
      temperature: new FormControl(null),
      heartRate: new FormControl(null),
      diaSis: new FormControl(null),
      indicators: new  FormControl(0),
      patientId: new FormControl()
    })
  }

  submit(){
    this.showSpinner = true;
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

    this.examinationForm.patchValue({indicators: ind})

    this.examinationService
      .createExamination(this.examinationForm.value)
      .subscribe(value => {
          this.showSpinner = false;
          this.router.navigate(['/examinations'])
        },
        error => {
          this.showSpinner = false;
          this.errors = error.error.errors
        });
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }
}
