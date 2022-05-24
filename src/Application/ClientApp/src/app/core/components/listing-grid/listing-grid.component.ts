import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { Listing } from 'src/app/api/types/listing';

import * as List from 'list.js';
import { ListingService } from 'src/app/api/services/listing.service';

@Component({
	selector: 'listing-grid',
	templateUrl: './listing-grid.component.html',
	styleUrls: ['./listing-grid.component.css']
})
export class ListingGridComponent implements OnInit {

	private list: List;
	private _listings: Listing[] = [];
	get listings(): Listing[] {
		return this._listings;
	}
	@Input() set listings(v: Listing[]) {
		if (v.length && v !== this._listings) {
			this._listings = v;
			setTimeout(() => this.initList(), 500);
		}
	}

	@Input() cityId: number = 0;
	@ViewChild('listingList', { static: false })
	private listingList: ElementRef;

	constructor(private readonly listingService: ListingService) { }

	ngOnInit() {

	}

	ngAfterViewInit() {
		if(this.listings.length == 0) this.loadListings();
	}

	private async loadListings(): Promise<void> {
		let listings = await this.listingService
			.getAllByCityId(this.cityId)
			.toPromise();
		this._listings = listings;
	}

	private initList(): void {
		var options = {
			valueNames: ['address', 'status', 'daysOnMarket', 'price', 'predictedRevenue', 'predVsPrice', 'highCapRate', 'middleCapRate', 'lowCapRate', 'longTermRate', 'bedrooms', 'bathrooms'],
			listClass: 'list',
			sortClass: 'list-sort'
		};

		this.list = new List(this.listingList.nativeElement, options);
	}

	async delete(l: Listing): Promise<void> {
		await this.listingService
			.delete(l.id)
			.toPromise();
		let index = this._listings.indexOf(l);
		this._listings.splice(index, 1);
	}
}
