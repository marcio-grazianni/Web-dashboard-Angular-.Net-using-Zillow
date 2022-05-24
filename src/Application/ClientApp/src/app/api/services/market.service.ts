import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { City } from '../types/city';
import { MarketHistory } from '../types/market-history';
import { State } from '../types/state';
import { BaseService } from './base.service';

@Injectable({
	providedIn: 'root'
})
export class MarketService extends BaseService {

	constructor(private readonly http: HttpClient) { super('/markets'); }

	public getAllStates(): Observable<State[]> {
		return new Observable<State[]>(o => {
			this.http.get<any[]>(`${this.apiUrl}/states`).subscribe(response => {
				o.next(State.fromJSCollection(response));
				o.complete();
			}, error => o.error(error))
		});
	}


	public getMarketByCode(code: string): Observable<City> {
		return new Observable<City>(o => {
			this.http.get<City>(`${this.apiUrl}/${code}`).subscribe(response => {
				let listing = City.fromJS(response);
				o.next(listing);
				o.complete();
			}, error => o.error(error))
		});
	}

	public getAllCitiesByStateCode(code: string): Observable<City[]> {
		return new Observable<City[]>(o => {
			this.http.get<City[]>(`${this.apiUrl}/state/${code}`).subscribe(response => {
				let listings = City.fromJSCollection(response);
				o.next(listings);
				o.complete();
			}, error => o.error(error))
		});
	}

	public getAllCities(details?: boolean): Observable<City[]> {
		return new Observable<City[]>(o => {
			let url = details ? `${this.apiUrl}?details=true` : this.apiUrl;
			this.http.get<City[]>(url).subscribe(response => {
				let listings = City.fromJSCollection(response);
				o.next(listings);
				o.complete();
			}, error => o.error(error))
		});
	}

	public getHistoryByCityId(id: number): Observable<MarketHistory> {
		return new Observable<MarketHistory>(o => {
			this.http.get<MarketHistory>(`${this.apiUrl}/history/${id}`).subscribe(response => {
				let history = MarketHistory.fromJS(response);
				o.next(history);
				o.complete();
			}, error => o.error(error))
		});
	}

	public getMarketCode(id: number): Observable<any[]> {
		return new Observable<any[]>(o => {
			this.http.get<any[]>(`${this.apiUrl}/marketcode/${id}`).subscribe(response => {
				o.next(response);
				o.complete();
			}, error => o.error(error))
		});
	}

	public saveMarketCode(data: any): Observable<any[]> {
		return new Observable<any[]>(o => {
			this.http.post<any[]>(`${this.apiUrl}/marketcode`, data).subscribe(response => {
				o.next(response);
				o.complete();
			}, error => o.error(error))
		});
	}
}
