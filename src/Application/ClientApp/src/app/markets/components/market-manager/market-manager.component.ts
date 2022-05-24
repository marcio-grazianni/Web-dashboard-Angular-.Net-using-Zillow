import { Component, OnInit } from '@angular/core';
import { MarketService } from 'src/app/api/services/market.service';
import { City } from 'src/app/api/types/city';

declare var $: any;

@Component({
  selector: 'app-market-manager',
  templateUrl: './market-manager.component.html',
  styleUrls: ['./market-manager.component.css']
})
export class MarketManagerComponent implements OnInit {

  city: City;
  cities: City[] = [];
  loading: boolean = false;
  constructor(private readonly marketService: MarketService) { }

  async ngOnInit() {
    await this.load();
  }

  async load(): Promise<void> {
    this.loading = true;
    this.cities = await this.marketService.getAllCities().toPromise();
    this.loading = false;
  }

  getCodes(c: City) : void {
    this.city = c;
    $('#market-code-finder').modal('show')
  }

}
