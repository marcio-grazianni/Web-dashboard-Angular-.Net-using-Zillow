import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ListingService } from 'src/app/api/services/listing.service';
import { MarketService } from 'src/app/api/services/market.service';
import { City } from 'src/app/api/types/city';

@Component({
	selector: 'app-state',
	templateUrl: './state.component.html',
	styleUrls: ['./state.component.css']
})
export class StateComponent implements OnInit {

	code: string;
	markets: City[] = [];
	get hasMarkets(): boolean {
		return this.markets.length > 0;
	}
	@ViewChild("chart", { static: true }) geochart: ElementRef;

	constructor(
		private readonly marketService: MarketService,
		private readonly listingService: ListingService,
		private readonly route: ActivatedRoute) { }

	async ngOnInit() {
		this.route.paramMap.subscribe(params => {
			this.code = params.get('code').toLocaleUpperCase();
			google.charts.setOnLoadCallback(() => this.load());
		});
	}


	async load(): Promise<void> {

		var source: any[][] = [['City', 'Listings']];
		var data = google.visualization.arrayToDataTable(source);

		var options: google.visualization.GeoChartOptions = {
			region: `US-${this.code}`,
			displayMode: 'regions',
			resolution: 'metros'
		};

		var chart = new google.visualization.GeoChart(this.geochart.nativeElement);
		chart.draw(data, options);


		var markets = await this.marketService
			.getAllCitiesByStateCode(this.code)
			.toPromise();

		this.markets = markets; //markets.filter(m => !!m.code);
	}

}
