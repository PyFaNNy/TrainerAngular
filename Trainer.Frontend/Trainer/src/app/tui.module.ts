import {TuiAxesModule, TuiLineChartModule} from '@taiga-ui/addon-charts';
import {NgModule} from "@angular/core";

@NgModule({
  exports: [
    TuiLineChartModule,
    TuiAxesModule
  ],
})
export class TuiModule {}
