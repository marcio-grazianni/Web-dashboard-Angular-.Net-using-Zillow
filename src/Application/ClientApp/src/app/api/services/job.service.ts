import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApifyRun } from '../types/apify-run';
import { Job } from '../types/job';
import { BaseService } from './base.service';

@Injectable({
	providedIn: 'root'
})
export class JobService extends BaseService {

	constructor(private readonly http: HttpClient) { super('/jobs'); }

	public getAll(): Observable<Job[]> {
		return new Observable<Job[]>(o => {
			this.http.get<any[]>(`${this.apiUrl}/`).subscribe(response => {
				let jobs = Job.fromJSCollection(response);
				o.next(jobs);
				o.complete();
			}, error => o.error(error))
		});
	}

	public getApifyRuns(): Observable<ApifyRun[]> {
		return new Observable<ApifyRun[]>(o => {
			this.http.get<any[]>(`${this.apiUrl}/apify`).subscribe(response => {
				let runs = ApifyRun.fromJSCollection(response);
				o.next(runs);
				o.complete();
			}, error => o.error(error))
		});
	}


	public deleteApifyRun(id: number): Observable<void> {
		return new Observable<void>(o => {
			this.http.delete<void>(`${this.apiUrl}/apify/${id}`).subscribe(response => {
				o.next();
				o.complete();
			}, error => o.error(error))
		});
	}
}
