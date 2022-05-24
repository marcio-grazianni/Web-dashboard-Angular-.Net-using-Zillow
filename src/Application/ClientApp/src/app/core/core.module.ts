import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavigationComponent } from './components/navigation/navigation.component';
import { RouterModule } from '@angular/router';
import { MarketHistoryComponent } from './components/market-history/market-history.component';
import { MarketRevenueComponent } from './components/market-revenue/market-revenue.component';
import { ListingGridComponent } from './components/listing-grid/listing-grid.component';
import { MarketYearlyRevenueComponent } from './components/market-yearly-revenue/market-yearly-revenue.component';
import { MarketCodeFinderComponent } from './components/market-code-finder/market-code-finder.component';
import { MarketSummaryComponent } from './components/market-summary/market-summary.component';
import { ListingSearchComponent } from './components/listing-search/listing-search.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [NavigationComponent, MarketHistoryComponent, MarketRevenueComponent, ListingGridComponent, MarketYearlyRevenueComponent, MarketCodeFinderComponent, MarketSummaryComponent, ListingSearchComponent],
  exports: [NavigationComponent, MarketHistoryComponent, MarketRevenueComponent, ListingGridComponent, MarketYearlyRevenueComponent, MarketCodeFinderComponent, MarketSummaryComponent, ListingSearchComponent],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule
  ]
})
export class CoreModule { }
