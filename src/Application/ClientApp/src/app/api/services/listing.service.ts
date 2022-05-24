import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LikeRequest } from '../types/like-request';


import { Listing } from '../types/listing';
import { BaseService } from './base.service';

@Injectable({
	providedIn: 'root'
})
export class ListingService extends BaseService {

	constructor(private readonly http: HttpClient) { super('/listings'); }

	public getAllByCityId(id: number, page: number = 0): Observable<Listing[]> {
		return new Observable<Listing[]>(o => {
			this.http.get<Listing[]>(`${this.apiUrl}/city/${id}?page=${page}`).subscribe(response => {
				let listings = Listing.fromJSCollection(response);
				o.next(listings);
				o.complete();
			}, error => o.error(error))
		});
	}

	public getById(id: number): Observable<Listing> {
		return new Observable<Listing>(o => {
			this.http.get<Listing>(`${this.apiUrl}/${id}`).subscribe(response => {
				let listing = Listing.fromJS(response);
				o.next(listing);
				o.complete();
			}, error => o.error(error))
		});
	}

	public like(dto: LikeRequest): Observable<void> {
		return new Observable<void>(o => {
			this.http.post<Listing>(`${this.apiUrl}/like`, dto).subscribe(response => {
				o.next();
				o.complete();
			}, error => o.error(error))
		});
	}

	public dislike(dto: LikeRequest): Observable<void> {
		return new Observable<void>(o => {
			this.http.post<Listing>(`${this.apiUrl}/dislike`, dto).subscribe(response => {
				o.next();
				o.complete();
			}, error => o.error(error))
		});
	}

	public getFavorites(id?: number): Observable<Listing[]> {
		return new Observable<Listing[]>(o => {
			this.http.get<Listing[]>(`${this.apiUrl}/liked`).subscribe(response => {
				let listings = Listing.fromJSCollection(response);
				o.next(listings);
				o.complete();
			}, error => o.error(error))
		});
	}


	public delete(id: number): Observable<boolean> {
		return new Observable<boolean>(o => {
			this.http.delete<void>(`${this.apiUrl}/${id}`).subscribe(response => {
				o.next(true);
				o.complete();
			}, error => o.error(error))
		});
	}

	public search(query: string): Observable<Listing[]> {
        return new Observable<Listing[]>(s => {
            this.http.get<Listing[]>(`${this.apiUrl}/search/${query}`).subscribe(response => {
				let listings = Listing.fromJSCollection(response);
				s.next(listings);
				s.complete();
			}, error => s.error(error))
        });
    }
	
}
