import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { HomeComponent } from './components/home/home.component';
import { MarketComponent } from './components/market/market.component';
import { CoreModule } from '../core/core.module';
import { ListingComponent } from './components/listing/listing.component';
import { FavoritesComponent } from './components/favorites/favorites.component';
import { StateComponent } from './components/state/state.component';
import { RouterModule } from '@angular/router';




@NgModule({
  declarations: [HomeComponent, MarketComponent, ListingComponent, FavoritesComponent, StateComponent],
  imports: [
    CommonModule,
    FormsModule,
    CoreModule,
    RouterModule
  ]
})
export class DashboardModule { }
