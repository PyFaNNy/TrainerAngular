import {Component, OnDestroy, OnInit} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {Subscription} from "rxjs";
import {PatientService} from "../../../services/patient.service";
import {Patient} from "../../../models/patient";
import {FormControl, FormGroup} from "@angular/forms";

@Component({
  selector: 'app-getPatients',
  templateUrl: './updatePatient.component.html'
})

export class UpdatePatientComponent implements OnInit, OnDestroy {
  patientForm: FormGroup;
  showSpinner: boolean = false;
  errors: any = null;
  subscriptions: Subscription = new Subscription();

  constructor(private route: ActivatedRoute, private patientService: PatientService, private router: Router) {
    this._createForm()
    this.subscriptions.add(route.params.subscribe(params => this.patientForm.patchValue({id: params['id']})));
  }

  ngOnInit(): void {
    this.subscriptions.add(this.patientService
      .getPatient(this.patientForm.value.id)
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
    this.showSpinner = true;
    this.subscriptions.add(this.patientService
      .updatePatient(this.patientForm.value)
      .subscribe(
        result => {
          this.showSpinner = false;
          this.router.navigate(['/patients'])
        },
        error => {
          this.showSpinner = false;
          this.errors = error.error.errors
        }));
  }
}

