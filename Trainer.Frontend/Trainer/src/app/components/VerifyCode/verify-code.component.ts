import {Component, OnDestroy} from "@angular/core";
import {Otp} from "../../models/otp";
import {ActivatedRoute, Router} from "@angular/router";
import {Subscription} from "rxjs";
import {OtpService} from "../../services/otp.service";

@Component({
  selector: 'app-reset-password',
  templateUrl: './verify-code.component.html'
})

export class VerifyCodeComponent implements OnDestroy{
  showSpinner: boolean;
  otp: Otp = new Otp;
  errors: any;
  subscriptions: Subscription = new Subscription();

  constructor(private route: ActivatedRoute, private otpService: OtpService,private router: Router){
    this.subscriptions.add(route.params.subscribe(params=>this.otp.email=params['email']));
    this.subscriptions.add(route.params.subscribe(params=>this.otp.action=params['action']));
  }

  verify()
  {
    this.showSpinner = true;
    this.subscriptions.add(this.otpService
    .verify(this.otp)
    .subscribe(
      result  => {
        this.showSpinner = false;
        if(this.otp.action == 'ResetPassword')
        {
          this.router.navigate(['/resetPassword',this.otp.email])
        }
        if(this.otp.action == 'Login')
        {
          this.router.navigate(['/home'])
        }
        if(this.otp.action == 'Registration')
        {
          this.router.navigate(['/login'])
        }
      },
      error => {
        this.showSpinner = false;
        this.errors = error.error
      }));
  };

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }
}
