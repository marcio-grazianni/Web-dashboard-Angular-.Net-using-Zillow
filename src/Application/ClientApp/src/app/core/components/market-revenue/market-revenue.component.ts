import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';

import * as Chart from 'chart.js';
import { MarketHistory } from 'src/app/api/types/market-history';
import { formatNumber } from '@angular/common';

@Component({
	selector: 'market-revenue',
	templateUrl: './market-revenue.component.html',
	styleUrls: ['./market-revenue.component.css']
})
export class MarketRevenueComponent implements OnInit {

	@Input() history: MarketHistory;
	@Input() bedrooms: number;
	private chart: Chart;
	@ViewChild('chartCanvas', { static: true })
	private chartCanvas: ElementRef;

	constructor() { }

	ngOnInit() {
		this.initChart();
	}

	private initChart(): void {
		let labels: string[] = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
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
				labels: labels,
				datasets: []
			}
		});
	}

	update(): void {
		this.chart.data.datasets = this.getDataset();
		this.chart.update();
	}

	private getDataset(): any[] {
		return [
			{
				label: 'Low',
				backgroundColor: '#D2DDEC',
				data: this.history.getMonthlyRevenueByBedrooms(this.bedrooms, 1)
			},
			{
				label: 'Middle',
				backgroundColor: '#A6C5F7',
				data: this.history.getMonthlyRevenueByBedrooms(this.bedrooms, 2)
			},
			{
				label: 'High',
				backgroundColor: '#2C7BE5',
				data: this.history.getMonthlyRevenueByBedrooms(this.bedrooms, 3)
			}
		];
	}

}
