import {Component, OnDestroy, OnInit} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {PatientService} from "../../../services/patient.service";
import {Subscription} from "rxjs";
import {Patient} from "../../../models/patient";
import {TokenService} from "../../../services/token.service";

@Component({
  selector: 'app-getPatient',
  templateUrl: './getPatient.component.html'
})

export class GetPatientComponent implements OnInit, OnDestroy {
  isDoctor?: boolean;
  id: string = "";
  patient: Patient = new Patient;
  results: any;
  subscriptions: Subscription = new Subscription();
  displayedColumns?: string[];

  constructor(private route: ActivatedRoute,
              private patientService: PatientService,
              private router: Router,
              private tokenSerbive: TokenService) {
    this.subscriptions.add(route.params.subscribe(params => this.id = params['id']));
  }

  ngOnInit(): void {
    this.subscriptions.add(this.patientService
      .getPatient(this.id)
      .subscribe((result: any) => {
        this.patient = result;
        this.results = result.results
      }));
    this.displayedColumns = ['typePhysicalActive', 'date', 'dia', 'sis', 'oxigen', 'temperature', 'heartRate'];

    let role = this.tokenSerbive.decodeToken(this.tokenSerbive.getToken()).role;
    this.isDoctor = role == 'doctor';
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }
}
