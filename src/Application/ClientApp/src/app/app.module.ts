import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { CoreModule } from './core/core.module';
import { DashboardModule } from './dashboard/dashboard.module';
import { MarketsModule } from './markets/markets.module';
import { TasksModule } from './tasks/tasks.module';

import { AppComponent } from './app.component';
import { HomeComponent } from './dashboard/components/home/home.component';
import { MarketComponent } from './dashboard/components/market/market.component';
import { SystemComponent } from './tasks/components/system/system.component';
import { ApifyComponent } from './tasks/components/apify/apify.component';
import { MarketManagerComponent } from './markets/components/market-manager/market-manager.component';
import { ListingComponent } from './dashboard/components/listing/listing.component';
import { FavoritesComponent } from './dashboard/components/favorites/favorites.component';
import { StateComponent } from './dashboard/components/state/state.component';




@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'state/:code', component: StateComponent },
      { path: 'market/:code', component: MarketComponent },
      { path: 'listing/:id', component: ListingComponent },
      { path: 'favorites', component: FavoritesComponent },
      {
        path: 'admin', children: [
          {
            path: 'markets', component: MarketManagerComponent
          },
          {
            path: 'tasks', children: [
              {
                path: 'system', component: SystemComponent
              },
              {
                path: 'apify', component: ApifyComponent
              }
            ]
          }
        ]
      }
    ]),
    CoreModule,
    DashboardModule,
    MarketsModule,
    TasksModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
