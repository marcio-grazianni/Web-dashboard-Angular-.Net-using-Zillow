import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MarketManagerComponent } from './components/market-manager/market-manager.component';
import { CoreModule } from '../core/core.module';



@NgModule({
  declarations: [MarketManagerComponent],
  imports: [
    CommonModule,
    CoreModule
  ]
})
export class MarketsModule { }
