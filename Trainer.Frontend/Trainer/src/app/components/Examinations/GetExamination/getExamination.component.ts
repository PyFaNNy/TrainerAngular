import {AfterContentInit, Component, OnInit, TemplateRef, ViewChild, ViewContainerRef} from "@angular/core";
import {TUI_DEFAULT_STRINGIFY, TuiContextWithImplicit, TuiStringHandler} from "@taiga-ui/cdk";
import {TuiPoint} from "@taiga-ui/core";
import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import {environment} from "../../../../environments/environment";
import {ActivatedRoute} from "@angular/router";
import {Subscription} from "rxjs";
import {MatDialog} from '@angular/material/dialog';
import {ExaminationDialogComponent} from "./ExaminationDialog/examination-dialog.component.";
import {ExaminationService} from "../../../services/examination.service";

@Component({
  selector: 'app-getExamination',
  templateUrl: './getExamination.component.html',
  styleUrls: ['./getExamination.component.scss']
})

export class GetExaminationComponent implements OnInit, AfterContentInit {
  sensor1: number = 0;
  sensor2: number = 0;
  sensor3: number = 0;
  sensor4: number = 0;

  tonometrOn: boolean = false;
  termometrOn: boolean = false;
  oximetrOn: boolean = false;
  heartRaterOn: boolean = false;

  statusTonometr: number = 0;
  statusTermometr: number = 0;
  statusOximetr: number = 0;
  statusHeatRate: number = 0;

  readonly valueHeatRate: TuiPoint[] = [];
  readonly valueTonometr: TuiPoint[][] = [[], []];
  readonly valueTermometr: TuiPoint[] = [];
  readonly valueOximetr: TuiPoint[] = [];

  @ViewChild("heartRateContainer", {read: ViewContainerRef})
  heartRateContainer!: ViewContainerRef;
  @ViewChild("heartRateTemplate", {read: TemplateRef})
  heartRateTemplate!: TemplateRef<any>;

  @ViewChild("tonometrContainer", {read: ViewContainerRef})
  tonometrContainer!: ViewContainerRef;
  @ViewChild("tonometrTemplate", {read: TemplateRef})
  tonometrTemplate!: TemplateRef<any>;

  @ViewChild("oximetrContainer", {read: ViewContainerRef})
  oximetrContainer!: ViewContainerRef;
  @ViewChild("oximetrTemplate", {read: TemplateRef})
  oximetrTemplate!: TemplateRef<any>;
  @ViewChild("termometrConteiner", {read: ViewContainerRef})
  termometrConteiner!: ViewContainerRef;
  @ViewChild("termometrTemplate", {read: TemplateRef})
  termometrTemplate!: TemplateRef<any>;

  readonly stringify = TUI_DEFAULT_STRINGIFY;
  private hubConnectionBuilder!: HubConnection;
  dialogData: any;
  private examinationId: string = '';
  subscriptions: Subscription = new Subscription();

  constructor(private route: ActivatedRoute, public dialog: MatDialog, private examinationService: ExaminationService) {
    this.subscriptions.add(route.params.subscribe(params => this.examinationId = params['id']));
  }

  ngAfterContentInit() {
    this.heartRateContainer?.createEmbeddedView(this.heartRateTemplate);
    this.tonometrTemplate?.createEmbeddedView(this.tonometrTemplate);
    this.termometrTemplate?.createEmbeddedView(this.termometrTemplate);
    this.oximetrTemplate?.createEmbeddedView(this.oximetrTemplate);
  }

  private rerender(container: any, template: any) {
    container.clear();
    container.createEmbeddedView(template);
  }

  openDialog() {
    this.dialog.open(ExaminationDialogComponent, {
      data: this.dialogData
    });
  }

  ngOnInit(): void {
    this.hubConnectionBuilder = new HubConnectionBuilder().withUrl(`${environment.apiUrl}/chart`).build();
    this.hubConnectionBuilder.start().then(() => console.log('Connection started.......!')).catch(err => console.log('Error while connect with server'));

    this.hubConnectionBuilder.on('newTonom', (time: number, dia: number, sis: number) => {
      this.valueTonometr[0].push([time * 10, dia]);
      this.valueTonometr[1].push([time * 10, sis]);
      this.rerender(this.tonometrContainer, this.tonometrTemplate);

    })
    this.hubConnectionBuilder.on('newTermom', (time: number, temp: number) => {
      this.valueTermometr.push([time * 10, temp]);
      this.rerender(this.termometrConteiner, this.termometrTemplate);
    })

    this.hubConnectionBuilder.on('newHearRate', (time: number, heartRate: number) => {
      this.valueHeatRate.push([time * 10, heartRate]);
      this.rerender(this.heartRateContainer, this.heartRateTemplate);
    })

    this.hubConnectionBuilder.on('newOximetr', (time: number, separation: number) => {
      this.valueOximetr.push([time, separation]);
      this.rerender(this.oximetrContainer, this.oximetrTemplate);
    })

    this.hubConnectionBuilder.on('statusTonometr', (status: string, count: number) => {
      this.statusTonometr = count;
      window.alert("Tonometr " + status)
    })

    this.hubConnectionBuilder.on('statusTermometr', (status: string, count: number) => {
      this.statusTermometr = count;
      window.alert("Termometr " + status)
    })

    this.hubConnectionBuilder.on('statusHeartrate', (status: string, count: number) => {
      this.statusTermometr = count;
      window.alert("Heartrate " + status)
    })

    this.hubConnectionBuilder.on('statusOximetr', (status: number, count: number) => {
      this.statusTermometr = count;
      window.alert("Oximetr " + status)
    })

    this.hubConnectionBuilder.on('error', (messange: string) => {
      window.alert(messange)
    })

    this.tonometrOn = false;
    this.termometrOn = false;
    this.oximetrOn = false;
    this.heartRaterOn = false;

    this.sensor1 = 0;
    this.sensor2 = 0;
    this.sensor3 = 0;
    this.sensor4 = 0;

    this.subscriptions.add(this.examinationService
      .getExamination(this.examinationId)
      .subscribe((result: any) => {
        this.dialogData = result
      }));
  }

  public testTonometr(): void {
    this.hubConnectionBuilder.invoke("TestTonomert");
  }

  public testTermometr(): void {
    this.hubConnectionBuilder.invoke("TestTermometr");
  }

  public testHeartrate(): void {
    this.hubConnectionBuilder.invoke("TestHeartrate");
  }

  public testOximetr(): void {
    this.hubConnectionBuilder.invoke("TestOximetr");
  }

  public start(): void {
    console.log(this.sensor1);
    console.log(this.statusTonometr);
    console.log(this.tonometrOn);
    this.hubConnectionBuilder.invoke("ProvideReading",
      this.statusTonometr, this.statusTermometr, this.statusHeatRate, this.statusOximetr,
      this.sensor1, this.sensor2, this.sensor3, this.sensor4,
      this.tonometrOn, this.termometrOn, this.heartRaterOn, this.oximetrOn, this.examinationId);
  }

  public tonometrClick() {
    this.tonometrOn = !this.tonometrOn;
  }

  public termometrClick() {
    this.termometrOn = !this.termometrOn;
  }

  public heartrateClick() {
    this.heartRaterOn = !this.heartRaterOn;
  }

  public oximetrClick() {
    this.oximetrOn = !this.oximetrOn;
  }

  readonly hint: TuiStringHandler<TuiContextWithImplicit<readonly TuiPoint[]>>
    = ({$implicit,}) => `${$implicit[0][0]} items:\n\n${$implicit.map(([_, y]) => y).join(`$\n`)}$`;
}
