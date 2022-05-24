import { Component, OnInit } from '@angular/core';
import { MarketService } from 'src/app/api/services/market.service';
import { State } from 'src/app/api/types/state';

@Component({
  selector: 'navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  states: State[] = [];
  
  get hasStates(): boolean {
    return this.states.length > 0;
  }
  
  constructor(private readonly marketService: MarketService) { }

  ngOnInit() {
    this.load();
  }


  async load(): Promise<void> {
    // this.markets = await this.marketService
    //   .getAllCities()
    //   .toPromise();
    // this.markets = this.markets.filter(m => !!m.code);
    this.states = await this.marketService
      .getAllStates()
      .toPromise();
  }

}
