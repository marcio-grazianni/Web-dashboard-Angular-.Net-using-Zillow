import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { DomSanitizer, SafeStyle } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';

import { ListingService } from 'src/app/api/services/listing.service';
import { LikeRequest } from 'src/app/api/types/like-request';
import { Listing } from 'src/app/api/types/listing';

@Component({
	selector: 'app-listing',
	templateUrl: './listing.component.html',
	styleUrls: ['./listing.component.css']
})
export class ListingComponent implements OnInit {

	loading: boolean;
	listing: Listing;

	@ViewChild('cover', { static: true }) cover: ElementRef;

	coverStyle: SafeStyle;

	priceHistory: any[] = [];
	taxHistory: any[] = [];
	mortgageRates: any = null;

	get profilePicture(): string {
		return this.listing ? this.listing.profilePicture : '';
	}

	get liked(): boolean {
		return this.listing ? this.listing.liked : false;
	}

	get hasPriceHistory(): boolean {
		return this.priceHistory.length > 0;
	}

	get hasTaxHistory(): boolean {
		return this.taxHistory.length > 0;
	}

	get hasMortgageRates(): boolean {
		return this.mortgageRates !== null;
	}

	loanOptions: LoanOption[] = [];
	mortgage: Mortage;

	constructor(
		private readonly listingService: ListingService,
		private readonly route: ActivatedRoute,
		private readonly sanitizer: DomSanitizer) { }

	async ngOnInit() {
		this.route.paramMap.subscribe(params => {
			this.loadData(params.get('id'));
		});
	}

	async loadData(id: string): Promise<void> {
		this.loading = true;
		this.listing = await this.listingService
			.getById(parseInt(id))
			.toPromise();
		console.log(this.listing);
		this.taxHistory = this.listing.details.taxHistory;
		this.priceHistory = this.listing.details.priceHistory;
		this.mortgageRates = this.listing.details.mortgageRates;
		this.initMortgageCalculator();
		this.coverStyle = this.sanitizer.bypassSecurityTrustStyle(`background-image: url(${this.listing.coverPicture}); height: 400px;`);
		this.loading = false;
	}


	async like(): Promise<void> {
		let request = new LikeRequest({
			entityId: this.listing.id
		});
		await this.listingService.like(request).toPromise();
		this.listing.liked = true;
	}

	async dislike(): Promise<void> {
		let request = new LikeRequest({
			entityId: this.listing.id
		});
		await this.listingService.dislike(request).toPromise();
		this.listing.liked = false;
	}

	
	selectedLoanOptionId: string = '0';
	private initMortgageCalculator(): void {
		if (!this.mortgageRates) return;
		this.loanOptions = [
			{
				id: '0',
				name: '30 Years',
				interestRate: this.mortgageRates.thirtyYearFixedRate,
				lengthOfLoan: 30
			},
			{
				id: '1',
				name: '15 Years',
				interestRate: this.mortgageRates.fifteenYearFixedRate,
				lengthOfLoan: 15
			}
		];
		this.mortgage = new Mortage(this.listing.price);
		this.estimateMonthlyPayment();
	}

	estimateMonthlyPayment(): void {
		let option = this.loanOptions.find(o => o.id === this.selectedLoanOptionId);
		this.mortgage.compute(option);
	}

}


interface LoanOption {
	id: string;
	name: string;
	interestRate: number;
	lengthOfLoan: number;
}


// https://www.bankrate.com/calculators/mortgages/mortgage-calculator.aspx
class Mortage {
	homePrice: number;
	downPayment: number;
	loanOption: LoanOption;
	estimatedMonthlyPayment: number;

	get payment(): number {
		return this.downPayment ? this.downPayment : 0;
	}
	get interestRate(): number {
		return this.loanOption ? this.loanOption.interestRate : 0;
	}
	get lengthOfLoan(): number {
		return this.loanOption ? this.loanOption.lengthOfLoan : 0;
	}

	constructor(price: number) {
		this.downPayment = price * .25;
		this.homePrice = price;
		this.estimatedMonthlyPayment = 0;
	}

	compute(option: LoanOption) : void {
		this.loanOption = option;
		let p = this.homePrice - this.downPayment;
		let r = this.interestRate / 1200;
		let n = this.lengthOfLoan * 12;

		let x = Math.pow((1 + r), n);
		
		let m = (r * x) / (x - 1);

		this.estimatedMonthlyPayment = p * m;
	}
}