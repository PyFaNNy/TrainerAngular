import {Component, OnDestroy, OnInit} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {Subscription, take} from "rxjs";
import {PatientService} from "../../../services/patient.service";
import {Patient} from "../../../models/patient";
import {FormControl, FormGroup} from "@angular/forms";

@Component({
  selector: 'app-getPatients',
  templateUrl: './updatePatient.component.html'
})

export class UpdatePatientComponent implements OnInit, OnDestroy {
  patientForm: FormGroup;
  id: string = "";
  errors: any = null;
  subscriptions: Subscription = new Subscription();

  constructor(private route: ActivatedRoute, private patientService: PatientService, private router: Router) {
    this.subscriptions.add(route.params.subscribe(params => this.id = params['id']));
    this._createForm()
  }

  ngOnInit(): void {
    this.subscriptions.add(this.patientService
      .getPatient(this.id)
      .subscribe((result: any) => {
        this._setData(result);
      }));
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

  private _createForm() {
    this.patientForm = new FormGroup({
      id: new FormControl(),
      firstName: new FormControl(),
      lastName: new FormControl(),
      middleName: new FormControl(),
      email: new FormControl(),
      age: new FormControl(),
      sex: new FormControl(),
    })
  }

  private _setData(data: Patient) {
    this.patientForm.setValue(
      {
        id: data.id,
        firstName: data.firstName,
        lastName: data.lastName,
        middleName: data.middleName,
        email: data.email,
        age: data.age,
        sex: data.sex,
      }
    )
  }

  submit() {
    this.subscriptions.add(this.patientService
      .updatePatient(this.patientForm.value)
      .subscribe(
        result => {
          this.router.navigate(['/patients'])
        },
        error => {
          this.errors = error.error.errors
        }));
  }
}

