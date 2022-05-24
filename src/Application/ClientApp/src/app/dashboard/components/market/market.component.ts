import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ListingService } from 'src/app/api/services/listing.service';
import { MarketService } from 'src/app/api/services/market.service';
import { City } from 'src/app/api/types/city';
import { Listing } from 'src/app/api/types/listing';
import { MarketHistory } from 'src/app/api/types/market-history';
import { MarketHistoryComponent } from 'src/app/core/components/market-history/market-history.component';
import { MarketRevenueComponent } from 'src/app/core/components/market-revenue/market-revenue.component';

@Component({
	selector: 'market',
	templateUrl: './market.component.html',
	styleUrls: ['./market.component.css']
})
export class MarketComponent implements OnInit {

	bedrooms: number = 1;
	market: City;
	history: MarketHistory;
	listings: Listing[] = [];

	get name(): string {
		return this.market ? this.market.name : '';
	}

	get hasData(): boolean {
		return false;
	}

	private revenue: Map<number, number[]>;

	@ViewChild('historyChart', { static: true }) historyChart: MarketHistoryComponent;

	constructor(
		private readonly marketService: MarketService,
		private readonly listingService: ListingService,
		private readonly route: ActivatedRoute) { }

	async ngOnInit() {
		this.route.paramMap.subscribe(params => {
			this.listings = [];
			this.loadData(params.get('code'));
		});
	}

	async loadData(code: string): Promise<void> {
		this.market = await this.marketService
			.getMarketByCode(code)
			.toPromise();
		await this.loadHistory();
		this.update();
	}


	selectedBedroomsChanged(v: number): void {
		this.bedrooms = v;
		setTimeout(() => this.update(), 300);
	}

	private update(): void {
		if(this.historyChart) this.historyChart.update();
	}

	private async loadHistory(): Promise<void> {
		if(!this.hasData) return;
		this.history = await this.marketService.getHistoryByCityId(this.market.id).toPromise();

		let rooms = 6;
		this.revenue = new Map<number, number[]>();
		for(let i = 1; i <= rooms; i++) {
			var grossRevenues = [];
			for(let j = 1; j <= 3; j++) {
				grossRevenues.push(this.history.getYearlyRevenueByBedrooms(i, j));
			}
			this.revenue.set(i, grossRevenues);
		}
	}
}
