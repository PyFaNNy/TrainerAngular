import {Component} from "@angular/core";
import {ExaminationService} from "../../../services/examination.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-importExaminations',
  templateUrl: './importExaminations.component.html'
})

export class ImportExaminationsComponent {
  file:any;
  errors:any;
  constructor(private examinationService: ExaminationService,private router: Router) {}

  getFile(event:any)
  {
    this.file = event.target.files[0];
  }

  submitData()
  {
    let formData  = new FormData();
    formData.set('name', 'examinations');
    formData.set('file', this.file);

    this.examinationService
      .load(formData)
      .subscribe(value => {
          this.router.navigate(['/examinations'])
        },
        error => {
          this.errors = error.error.errors
        });
  }
}
