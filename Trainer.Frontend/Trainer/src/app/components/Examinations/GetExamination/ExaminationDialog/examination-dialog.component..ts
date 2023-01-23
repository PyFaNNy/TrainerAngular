import {Component, Inject, OnInit} from "@angular/core";

import {ActivatedRoute} from "@angular/router";
import {Subscription} from "rxjs";
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';

@Component({
  selector: 'app-dialogExamination',
  templateUrl: './examination-dialog.component.html',
  styleUrls: ['./examination-dialog.component..scss']
})

export class ExaminationDialogComponent implements OnInit {
  examination:any;
  constructor(@Inject(MAT_DIALOG_DATA) private data: any,
              private dialogRef: MatDialogRef<ExaminationDialogComponent>) {
    this.examination = data;
  }

  ngOnInit(): void {
  }

  close() {
    this.dialogRef.close();
  }
}
