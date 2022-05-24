import { formatNumber } from '@angular/common';
import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import * as Chart from 'chart.js';
import { MarketService } from 'src/app/api/services/market.service';
import { City } from 'src/app/api/types/city';
import { HistoryRecord, MarketHistory } from 'src/app/api/types/market-history';

@Component({
	selector: 'market-history',
	templateUrl: './market-history.component.html',
	styleUrls: ['./market-history.component.css']
})
export class MarketHistoryComponent implements OnInit {

	private selectedBedroom: number = 1;
	private chart: Chart;
	@Input() history: MarketHistory;
	@Input() city: City;
	@Output() selectedBedroomsChange: EventEmitter<number> = new EventEmitter<number>();
	get marketName(): string {
		return this.city ? this.city.marketName : '';
	}

	options: any[] = [];

	@ViewChild('chartCanvas', { static: true }) chartCanvas: ElementRef;

	constructor() { }

	ngOnInit() {
		this.options = [
			{ value: 1, text: "Single" },
			{ value: 2, text: "Two" },
			{ value: 3, text: "Three" },
			{ value: 4, text: "Four" },
			{ value: 5, text: "Five" },
			{ value: 6, text: "Six" }];
		this.initChart();
	}

	ngAfterViewInit(): void {

	}

	selectOption(v: number): void {
		this.selectedBedroom = v;
		this.update();
		this.selectedBedroomsChange.emit(v);
	}

	isSelectedOption(v: number): boolean {
		return this.selectedBedroom === v;
	}

	private initChart(): void {
		let labels: string[] = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

		// this.chart = new Chart(this.chartCanvas.nativeElement, {
		// 	type: 'line',
		// 	options: {
		// 		scales: {
		// 			yAxes: [
		// 				{
		// 					id: 'pricingYAxis',
		// 					type: 'linear',
		// 					display: 'auto',
		// 					gridLines: {
		// 						color: '#283E59',
		// 						zeroLineColor: '#283E59'
		// 					},
		// 					ticks: {
		// 						callback: function (value) {
		// 							return "$" + value;
		// 						},
		// 						// min: max,
		// 						// max: min
		// 					}
		// 				},
		// 				{
		// 					id: 'occupancyYAxis',
		// 					type: 'linear',
		// 					display: 'auto',
		// 					gridLines: {
		// 						color: '#283E59',
		// 						zeroLineColor: '#283E59'
		// 					},
		// 					position: 'right',
		// 					ticks: {
		// 						callback: function (value) {
		// 							return value + '%';
		// 						}
		// 					}
		// 				}]
		// 		}
		// 	},
		// 	data: {
		// 		labels: labels,
		// 		datasets: []
		// 	}
		// });

		this.chart = new Chart(this.chartCanvas.nativeElement, {
			type: 'bar',
			options: {
				scales: {
					yAxes: [{
						id: 'pricingYAxis',
						type: 'linear',
						display: 'auto',
						gridLines: {
							color: '#283E59',
							zeroLineColor: '#283E59'
						},
						ticks: {
							beginAtZero: true,
							callback: function (value: number) {
								return '$' + formatNumber(value, 'en_US');
							}
						}
					}]
				}
			},
			data: {
				labels: labels,
				datasets: []
			}
		});
	}

	// update(): void {
	// 	let pricingData = this.history.getPricingByBedrooms(this.selectedBedroom);
	// 	let occupancyData = this.history.getOccupancyByBedrooms(this.selectedBedroom);

	// 	let pricing = this.getDatasets(pricingData, 'pricingYAxis', 'ADR');
	// 	let occupancy = this.getDatasets(occupancyData, 'occupancyYAxis', 'OCC');
	// 	occupancy.forEach(o => o.borderDash = [5, 5]);
	// 	let adr = [pricing[0]];
	// 	this.chart.data.datasets = adr.concat(occupancy)
	// 	this.chart.update();
	// }

	// private getDatasets(data: HistoryRecord[], yAxis: string, label: string): any[] {

	// 	let low = {
	// 		label: `50% ${label}`,
	// 		data: [],
	// 		yAxisID: yAxis,
	// 		borderColor: '#D2DDEC'
	// 	};

	// 	let middle = {
	// 		label: `75% ${label}`,
	// 		data: [],
	// 		yAxisID: yAxis,
	// 		borderColor: '#A6C5F7'
	// 	};

	// 	let high = {
	// 		label: `90% ${label}`,
	// 		data: [],
	// 		yAxisID: yAxis
	// 	};

	// 	data.forEach(d => {
	// 		low.data.push(d.percentiles[1]);
	// 		middle.data.push(d.percentiles[2]);
	// 		high.data.push(d.percentiles[3]);
	// 	});

	// 	return [low, middle, high];
	// }




	update(): void {
		this.chart.data.datasets = this.getDataset();
		this.chart.update();
	}

	private getDataset(): any[] {
		return [
			{
				label: 'Low',
				backgroundColor: '#D2DDEC',
				data: this.history.getMonthlyRevenueByBedrooms(this.selectedBedroom, 1)
			},
			{
				label: 'Middle',
				backgroundColor: '#A6C5F7',
				data: this.history.getMonthlyRevenueByBedrooms(this.selectedBedroom, 2)
			},
			{
				label: 'High',
				backgroundColor: '#2C7BE5',
				data: this.history.getMonthlyRevenueByBedrooms(this.selectedBedroom, 3)
			}
		];
	}

}