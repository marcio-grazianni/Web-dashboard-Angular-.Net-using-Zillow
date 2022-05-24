import { Component, OnInit } from '@angular/core';
import { JobService } from 'src/app/api/services/job.service';
import { Job } from 'src/app/api/types/job';

@Component({
  selector: 'app-system',
  templateUrl: './system.component.html',
  styleUrls: ['./system.component.css']
})
export class SystemComponent implements OnInit {

  private readonly _refreshRate: number = 60;
  jobs: Job[] = [];

  constructor(private readonly jobService: JobService) { }

  ngOnInit() {
    this.load();
    setInterval(() => this.load(), this._refreshRate * 1000);
  }

  async load(): Promise<void> {
    this.jobs = await this.jobService.getAll().toPromise();
  }

  async schedule(r: any): Promise<void> {
    let job = await this.jobService.schedule(r).toPromise();
    this.jobs.unshift(job);
  }

}
