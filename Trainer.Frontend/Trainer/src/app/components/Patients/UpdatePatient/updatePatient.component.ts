import {Component} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {Subscription, take} from "rxjs";
import {PatientService} from "../../../services/patient.service";
import {Patient} from "../../../models/patient";

@Component({
  selector: 'app-getPatients',
  templateUrl: './updatePatient.component.html'
})

export class UpdatePatientComponent {
  id: string ="";
  patient: Patient =new Patient;
  errors: any = null;
  subscriptions: Subscription = new Subscription();

  constructor(private route: ActivatedRoute, private patientService: PatientService,private router: Router){
    this.subscriptions.add(route.params.subscribe(params=>this.id=params['id']));
  }

  ngOnInit(): void {
    this.subscriptions.add(this.patientService
      .getPatient(this.id)
      .subscribe((result: any) => { this.patient = result}));
  }

  updatePatient() {
    this.subscriptions.add(this.patientService
      .updatePatient(this.patient)
      .subscribe(
        result  => {
          this.router.navigate(['/patients'])
        },
        error => {
          this.errors = error.error.errors
        }));
    };

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }
}

