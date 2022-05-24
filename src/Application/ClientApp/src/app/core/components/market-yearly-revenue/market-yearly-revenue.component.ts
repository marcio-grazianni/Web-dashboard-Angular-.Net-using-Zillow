import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { MarketHistory } from 'src/app/api/types/market-history';

import * as Chart from 'chart.js';
import { formatNumber } from '@angular/common';

@Component({
	selector: 'market-yearly-revenue',
	templateUrl: './market-yearly-revenue.component.html',
	styleUrls: ['./market-yearly-revenue.component.css']
})
export class MarketYearlyRevenueComponent implements OnInit {

	private _history: MarketHistory;
	@Input() set history(v: MarketHistory) {
		if (v) {
			this._history = v;
			this.update();
		}
	}
	get history(): MarketHistory {
		return this._history;
	}
	private chart: Chart;
	@ViewChild('chartCanvas', { static: true })
	private chartCanvas: ElementRef;
	@ViewChild('chartLegend', { static: true })
	private chartLegend: ElementRef;


	constructor() { }

	ngOnInit() {
		this.initChart();
	}

	private initChart(): void {
		// this.chart = new Chart(this.chartCanvas.nativeElement, {
		// 	type: 'doughnut',
		// 	options: {
		// 		tooltips: {
		// 			callbacks: {
		// 				label: function (tooltipItem, data) {
		// 					var dataset = data.datasets[tooltipItem.datasetIndex];
		// 					var value = dataset.data[tooltipItem.index];
		// 					return '$' + formatNumber(value, 'en_US');
		// 				}
		// 			}
		// 		}
		// 	},
		// 	data: {
		// 		labels: ['Single', 'Two', 'Three', 'Four', 'Five', 'Six'],
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
				labels: ['Single', 'Two', 'Three', 'Four', 'Five', 'Six'],
				datasets: []
			}
		});
	}

	update(): void {
		this.chart.data.datasets = this.getDataset();
		this.chart.update();
		//this.chartLegend.nativeElement.innerHTML = this.chart.generateLegend();
	}

	private getDataset(): any[] {
		let yRevenue = [];
		let total = 0;



		// return {
		// 	data: yRevenue,
		// 	backgroundColor: ['#727cf5', '#6b5eae', '#ff679b', '#fd7e14', '#00D97E', '#02a8b5']
		// }
		return [
			{
				label: 'Low',
				backgroundColor: '#D2DDEC',
				data: this.getStuff(1)
			},
			{
				label: 'Middle',
				backgroundColor: '#A6C5F7',
				data: this.getStuff(2)
			},
			{
				label: 'High',
				backgroundColor: '#2C7BE5',
				data: this.getStuff(3)
			}
		];
	}

	private getStuff(index: number): number[] {
		let data = [];
		for (let i = 1; i <= 6; i++) {
			data.push(this.history.getYearlyRevenueByBedrooms(i, index));
		}
		return data;
	}

}
