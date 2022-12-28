import {Component} from "@angular/core";
import {ActivatedRoute} from "@angular/router";
import {Subscription} from "rxjs";
import {PatientService} from "../../../services/patient.service";
import {Patient} from "../../../models/patient";

@Component({
  selector: 'app-getPatients',
  templateUrl: './updatePatient.component.html'
})

export class UpdatePatientComponent {
  id: string ="";
  patient: Patient =new Patient;
  private routeSubscription: Subscription;

  constructor(private route: ActivatedRoute, private patientService: PatientService){
    this.routeSubscription = route.params.subscribe(params=>this.id=params['id']);
  }

  ngOnInit(): void {
    this.patientService
      .getPatient(this.id)
      .subscribe((result: any) => ( this.patient = result));
  }
}
