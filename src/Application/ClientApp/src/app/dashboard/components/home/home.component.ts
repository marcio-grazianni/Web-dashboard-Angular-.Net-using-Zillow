import { Component, ElementRef, ViewChild } from '@angular/core';
import { ListingService } from 'src/app/api/services/listing.service';
import { MarketService } from 'src/app/api/services/market.service';
import { City } from 'src/app/api/types/city';
import { Listing } from 'src/app/api/types/listing';


@Component({
	selector: 'dashboard',
	templateUrl: './home.component.html',
})
export class HomeComponent {

	selectedCityId: number;
	cities: City[] = [];
	listings: Listing[] = [];
	@ViewChild("chart", { static: true }) geochart: ElementRef;
	constructor(private readonly marketService: MarketService, private readonly listingService: ListingService) { }

	ngOnInit(): void {

		google.charts.setOnLoadCallback(() => this.load());
	}
	
	async load(): Promise<void> {
		let states = await this.marketService
			.getAllStates()
			.toPromise();

		var source: any[][] = [['State', 'Listings']];
		states.forEach(s => source.push([`${s.name}`, s.totalListings]));

		var data = google.visualization.arrayToDataTable(source);

		var options: google.visualization.GeoChartOptions = {
			region: 'US',
			displayMode: 'regions',
			resolution: 'provinces',
			backgroundColor: '#f9fbfd'
			// colorAxis: { colors: ['green', 'blue'] }
		};

		var chart = new google.visualization.GeoChart(this.geochart.nativeElement);
		chart.draw(data, options);
		// this.cities = await this.marketService
		// 	.getAllCities()
		// 	.toPromise();
		// this.cities.unshift(new City({
		// 	id: 0,
		// 	name: 'All'
		// }));
		// this.selectedCityId = this.cities[0].id;
		// this.loadCities();
	}

	async loadCities(): Promise<void> {
		this.listings = [];
		this.listings = await this.listingService
			.getAllByCityId(this.selectedCityId)
			.toPromise();
	}
}
