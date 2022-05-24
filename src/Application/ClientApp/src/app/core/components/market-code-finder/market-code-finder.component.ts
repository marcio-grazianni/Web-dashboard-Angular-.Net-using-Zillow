import { Component, ElementRef, EventEmitter, Input, NgZone, OnInit, Output, ViewChild } from '@angular/core';
import { MarketService } from 'src/app/api/services/market.service';
import { City } from 'src/app/api/types/city';

declare var $: any;

@Component({
  selector: 'market-code-finder',
  templateUrl: './market-code-finder.component.html',
  styleUrls: ['./market-code-finder.component.css']
})
export class MarketCodeFinderComponent implements OnInit {

  @Input() market: City;
  @Output() marketChange: EventEmitter<City> = new EventEmitter<City>();

  @ViewChild('modal', { static: true })
  private modal: ElementRef;

  markets: any[] = [];
  private selectedMarket: any = null;

  get marketName(): string {
    return this.market ? this.market.name : '';
  }
  get hasNoSelection(): boolean {
    return this.selectedMarket === null;
  }
  constructor(private readonly ngZone: NgZone, private readonly marketService: MarketService) { }

  ngOnInit() {
  }


  ngAfterViewInit(): void {
    $(this.modal.nativeElement).on('show.bs.modal', (e: any) => {
      this.ngZone.run(() => {
        setTimeout(() => this.load(), 300);
      })
    });
    $(this.modal.nativeElement).on('hide.bs.modal', (e: any) => {
      this.ngZone.run(() => {
        this.markets = [];
        this.selectedMarket = null;
      })
    });
  }

  dismiss() {
    $(this.modal.nativeElement).modal('hide');
    //this.initialState();
  }


  async load(): Promise<void> {
    this.markets = await this.marketService.getMarketCode(this.market.id).toPromise();
    console.log(this.markets);
  }

  select(m: any) {
    this.selectedMarket = m;
  }

  isSelected(m: any): boolean {
    return this.selectedMarket === m;
  }


  async save(): Promise<void> {
    let payload = {
      cityId: this.market.id,
      id: this.selectedMarket.city.id,
      name: this.selectedMarket.name,
      code: this.selectedMarket.city.code
    };
    await this.marketService.saveMarketCode(payload).toPromise();
    this.market.marketId = payload.id;
    this.market.marketName = payload.name;
    this.market.code = payload.code;
    this.marketChange.emit(this.market);
    this.dismiss();

  }
}
