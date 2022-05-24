import { Component, OnInit } from '@angular/core';
import { JobService } from 'src/app/api/services/job.service';

@Component({
  selector: 'app-apify',
  templateUrl: './apify.component.html',
  styleUrls: ['./apify.component.css']
})
export class ApifyComponent implements OnInit {

  jobs: any[] = [];

  constructor(private readonly jobService: JobService) { }

  ngOnInit() {
    this.load();
  }

  async load(): Promise<void> {
    this.jobs = await this.jobService.getApifyRuns().toPromise();
  }


  async delete(job: any): Promise<void> {
    await this.jobService
      .deleteApifyRun(job.id)
      .toPromise();
  }


}
