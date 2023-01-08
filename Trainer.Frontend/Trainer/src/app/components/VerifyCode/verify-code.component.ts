import {Component} from "@angular/core";
import {Otp} from "../../models/otp";
import {ActivatedRoute, Router} from "@angular/router";
import {PatientService} from "../../services/patient.service";
import {Subscription} from "rxjs";
import {OtpService} from "../../services/otp.Service";

@Component({
  selector: 'app-reset-password',
  templateUrl: './verify-code.component.html'
})

export class VerifyCodeComponent {
  otp: Otp = new Otp;
  errors: any;
  subscriptions: Subscription = new Subscription();

  constructor(private route: ActivatedRoute, private otpService: OtpService,private router: Router){
    this.subscriptions.add(route.params.subscribe(params=>this.otp.email=params['email']));
    this.subscriptions.add(route.params.subscribe(params=>this.otp.action=params['action']));
  }

  verify()
  {
    this.subscriptions.add(this.otpService
    .verify(this.otp)
    .subscribe(
      result  => {
        this.router.navigate(['/home'])
      },
      error => {
        this.errors = error.error
      }));
  };

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }
}
