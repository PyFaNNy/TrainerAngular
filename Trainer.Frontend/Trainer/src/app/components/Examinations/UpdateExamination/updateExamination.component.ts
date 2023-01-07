import {Component} from "@angular/core";
import {Examination} from "../../../models/examination";
import {ActivatedRoute, Router} from "@angular/router";
import {Subscription} from "rxjs";
import {ExaminationService} from "../../../services/examination.service";

@Component({
  selector: 'app-updateExamination',
  templateUrl: './updateExamination.component.html'
})

export class UpdateExaminationComponent {
  examination: Examination = new Examination();
  diaSis:boolean = false;
  tempareture:boolean = false;
  heartRate:boolean = false;
  spO2:boolean = false;
  errors: any = null;
  subscriptions: Subscription = new Subscription();
  constructor(private route: ActivatedRoute, private examinationService: ExaminationService,private router: Router){
    this.subscriptions.add(route.params.subscribe(params=>this.examination.id=params['id']));
  }

  ngOnInit(): void {
    this.subscriptions.add(this.examinationService
      .getExamination(this.examination.id)
      .subscribe((result: any) => {
        console.log(result);
        this.examination = result
        console.log(this.examination);

        this.diaSis = result.diaSis;
        this.tempareture = result.tempareture;
        this.heartRate = result.heartRate;
        this.spO2 = result.sp02;
      }));
  }

  updateExamination() {
    if (this.diaSis) {
      this.examination.indicators += 1;
    }
    if (this.tempareture) {
      this.examination.indicators += 2;
    }
    if (this.heartRate) {
      this.examination.indicators += 4;
    }
    if (this.spO2) {
      this.examination.indicators += 8;
    }

    this.examinationService
      .updateExamination(this.examination)
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
