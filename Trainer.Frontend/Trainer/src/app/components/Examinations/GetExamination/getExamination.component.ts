import {Component} from "@angular/core";
import { TUI_DEFAULT_STRINGIFY } from "@taiga-ui/cdk";
import {TuiPoint} from "@taiga-ui/core";

@Component({
  selector: 'app-getExamination',
  templateUrl: './getExamination.component.html',
  styleUrls: ['./getExamination.component.scss']
})

export class GetExaminationComponent {
  sensor1: number =0;
  sensor2: number =0;
  sensor3: number =0;
  sensor4: number =0;

  readonly value: readonly TuiPoint[] = [
    [0, 25],
    [50, 50],
    [100, 25],
    [150, 90],
    [200, 130],
    [250, 140],
    [300, 90],
  ];

  readonly stringify = TUI_DEFAULT_STRINGIFY;
}
