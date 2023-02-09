import { Component } from '@angular/core';
import {ServerErrorDataService} from "../../services/server-error-data.service";

@Component({
  selector: 'app-error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.scss']
})
export class ErrorComponent {
  constructor(public errorData: ServerErrorDataService) {
  }
}
