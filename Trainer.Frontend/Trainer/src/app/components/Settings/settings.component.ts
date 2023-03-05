import {Component, OnInit} from "@angular/core";
import {SettingsService} from "../../services/settings.service";
import {FormControl, FormGroup} from "@angular/forms";

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss'],
})

export class SettingsComponent implements OnInit {
  examinationsErrorsForm: FormGroup;
  patientsErrorsForm: FormGroup;
  resultsErrorsForm: FormGroup;
  otpErrorsForm: FormGroup;
  usersErrorsForm: FormGroup;
  csvErrorsForm: FormGroup;

  constructor(private settingsService: SettingsService){
    this._createExaminationsErrorsForm();
    this._createOTPErrorsForm();
    this._createCSVErrorsForm();
    this._createPatientsErrorsForm();
    this._createUsersErrorsForm();
    this._createResultsErrorsForm();
  }

  ngOnInit(): void {
    this.settingsService
      .getPatientsErrorSettings()
      .subscribe((result: any) => {
        this._updatePatientsErrorsForm(result);
      });

    this.settingsService
      .getExaminationsErrorSettings()
      .subscribe((result: any) => {
        this._updateExaminationsErrorsForm(result);
      });

    this.settingsService
      .getUsersErrorSettings()
      .subscribe((result: any) => {
        console.log(result);
        this._updateUsersErrorsForm(result);
      });

    this.settingsService
      .getCSVErrorSettings()
      .subscribe((result: any) => {
        this._updateCSVErrorsForm(result);
      });

    this.settingsService
      .getOTPErrorSettings()
      .subscribe((result: any) => {
        this._updateOTPErrorsForm(result);
      });

    this.settingsService
      .getResultsErrorSettings()
      .subscribe((result: any) => {
        this._updateResultsErrorsForm(result);
      });

    console.log(this.usersErrorsForm.value)
  }

  submitExaminations() {
    this.settingsService
      .updateExaminationsErrorSettings(this.examinationsErrorsForm.value)
      .subscribe(value => {
        },
        error => {
        });
  }

  submitPatients() {
    this.settingsService
      .updatePatientsErrorSettings(this.patientsErrorsForm.value)
      .subscribe(value => {
        },
        error => {
        });
  }

  submitUsers() {
    this.settingsService
      .updateUsersErrorSettings(this.usersErrorsForm.value)
      .subscribe(value => {
        },
        error => {
        });
  }

  submitOTP() {
    this.settingsService
      .updateOTPErrorSettings(this.otpErrorsForm.value)
      .subscribe(value => {
        },
        error => {
        });
  }

  submitCSV() {
    this.settingsService
      .updateCSVErrorSettings(this.csvErrorsForm.value)
      .subscribe(value => {
        },
        error => {
        });
  }

  submitResult() {
    this.settingsService
      .updateResultsErrorSettings(this.resultsErrorsForm.value)
      .subscribe(value => {
        },
        error => {
        });
  }

  private _createExaminationsErrorsForm() {
    this.examinationsErrorsForm = new FormGroup({
      createExaminationEnable: new FormControl(),
      createEmailExaminationEnable: new FormControl(),
      deleteExaminationEnable: new FormControl(),
      finishExaminationEnable: new FormControl(),
      updateExaminationEnable: new FormControl(),
      updateEmailExaminationEnable: new FormControl(),
      getExaminationEnable: new FormControl(),
      getRandomExaminationEnable: new FormControl(),
      getExaminationsEnable: new FormControl(),
      getRandomExaminationsEnable: new FormControl(),
    })
  }
  private _createUsersErrorsForm() {
    this.usersErrorsForm = new FormGroup({
      approveUserEnable: new FormControl(),
      approveUserEmailEnable: new FormControl(),
      blockUserEnable: new FormControl(),
      blockUserEmailEnable: new FormControl(),
      declineUserEnable: new FormControl(),
      declineUserEmailEnable: new FormControl(),
      deleteUserEnable: new FormControl(),
      deleteUserEmailEnable: new FormControl(),
      unBlockUserEnable: new FormControl(),
      unBlockUserEmailEnable: new FormControl(),
      resetPasswordUserEnable: new FormControl(),
      getBaseUserEnable: new FormControl(),
      getRandomBaseUserEnable: new FormControl(),
      signInEnable: new FormControl(),
    })
  }
  private _createPatientsErrorsForm() {
    this.patientsErrorsForm = new FormGroup({
      createPatientEnable: new FormControl(),
      deletePatientEnable: new FormControl(),
      updatePatientEnable: new FormControl(),
      getPatientEnable: new FormControl(),
      getRandomPatientEnable: new FormControl(),
      getPatientsEnable: new FormControl(),
      getRandomPatientsEnable: new FormControl(),
    })
  }
  private _createOTPErrorsForm() {
    this.otpErrorsForm = new FormGroup({
      requestLoginCodeEnable: new FormControl(),
      requestRandomLoginCodeEnable: new FormControl(),
      requestPasswordEnable: new FormControl(),
      requestRandomPasswordEnable: new FormControl(),
      requestRegistrationCodeEnable: new FormControl(),
      requestRandomRegistrationCodeEnable: new FormControl(),
      isUniversalVerificationCodeEnabled: new FormControl(),
      universalVerificationCode: new FormControl(),
    })
  }
  private _createCSVErrorsForm() {
    this.csvErrorsForm = new FormGroup({
      csvToExaminationsEnable: new FormControl(),
      csvToPatientsEnable: new FormControl(),
    })
  }
  private _createResultsErrorsForm() {
    this.resultsErrorsForm = new FormGroup({
    })
  }

  private _updateExaminationsErrorsForm(result: any) {
    this.examinationsErrorsForm.patchValue(
      {
        createExaminationEnable:result.createExaminationEnable,
        createEmailExaminationEnable:result.createEmailExaminationEnable,
        deleteExaminationEnable:result.deleteExaminationEnable,
        finishExaminationEnable:result.finishExaminationEnable,
        updateExaminationEnable:result.updateExaminationEnable,
        updateEmailExaminationEnable:result.updateEmailExaminationEnable,
        getExaminationEnable:result.getExaminationEnable,
        getRandomExaminationEnable:result.getRandomExaminationEnable,
        getExaminationsEnable:result.getExaminationsEnable,
        getRandomExaminationsEnable:result.getRandomExaminationsEnable,
      }
    )
  }
  private _updateUsersErrorsForm(result: any) {
    this.usersErrorsForm.patchValue(
      {
        approveUserEnable:result.approveUserEnable,
        approveUserEmailEnable:result.approveUserEmailEnable,
        blockUserEnable:result.blockUserEnable,
        blockUserEmailEnable:result.blockUserEmailEnable,
        declineUserEnable:result.declineUserEnable,
        declineUserEmailEnable:result.declineUserEmailEnable,
        deleteUserEnable:result.deleteUserEnable,
        deleteUserEmailEnable:result.deleteUserEmailEnable,
        unBlockUserEnable:result.unBlockUserEnable,
        unBlockUserEmailEnable:result.unBlockUserEmailEnable,
        resetPasswordUserEnable:result.resetPasswordUserEnable,
        getBaseUserEnable:result.getBaseUserEnable,
        getRandomBaseUserEnable:result.getRandomBaseUserEnable,
        getBaseUsersEnable:result.getBaseUsersEnable,
        getRandomBaseUsersEnable:result.getRandomBaseUsersEnable,
        signInEnable:result.signInEnable,
      }
    )
  }
  private _updatePatientsErrorsForm(result: any) {
    this.patientsErrorsForm.patchValue(
      {
        createPatientEnable: result.createPatientEnable,
        deletePatientEnable: result.deletePatientEnable,
        updatePatientEnable: result.updatePatientEnable,
        getPatientEnable: result.getPatientEnable,
        getRandomPatientEnable: result.getRandomPatientEnable,
        getPatientsEnable: result.getPatientsEnable,
        getRandomPatientsEnable: result.getRandomPatientsEnable,
      }
    )
  }
  private _updateOTPErrorsForm(result: any) {
    this.otpErrorsForm.patchValue(
      {
        requestLoginCodeEnable:result.requestLoginCodeEnable,
        requestRandomLoginCodeEnable:result.requestRandomLoginCodeEnable,
        requestPasswordEnable:result.requestPasswordEnable,
        requestRandomPasswordEnable:result.requestRandomPasswordEnable,
        requestRegistrationCodeEnable:result.requestRegistrationCodeEnable,
        requestRandomRegistrationCodeEnable:result.requestRandomRegistrationCodeEnable,
        isUniversalVerificationCodeEnabled:result.isUniversalVerificationCodeEnabled,
        universalVerificationCode:result.universalVerificationCode,
      }
    )
  }
  private _updateCSVErrorsForm(result: any) {
    this.csvErrorsForm.patchValue(
      {
        csvToExaminationsEnable:result.csvToExaminationsEnable,
        csvToPatientsEnable:result.csvToPatientsEnable,
      }
    )
  }
  private _updateResultsErrorsForm(result: any) {

  }
}
