import { Component, ElementRef, EventEmitter, NgZone, OnInit, Output, ViewChild } from '@angular/core';
import { JobRequest, JobType } from 'src/app/api/types/job-request';

declare var $: any;

@Component({
  selector: 'job-scheduler',
  templateUrl: './process-scheduler.component.html',
  styleUrls: ['./process-scheduler.component.css']
})
export class ProcessSchedulerComponent implements OnInit {

  types: any[] = [
    {
      type: JobType.ListingDiscovery,
      name: 'Listing Discovery',
      title: 'Sync Listings from Zillow'
    },
    {
      type: JobType.Calculation,
      name: 'Calculation',
      title: 'Run Calculations'
    },
    {
      type: JobType.MarketSummaryDiscovery,
      name: 'Markey Summary Discovery',
      title: 'Get Market Summary'
    }
  ]
  request: JobRequest;
  @ViewChild('modal', { static: true })
  private modal: ElementRef;

  @Output() schedule: EventEmitter<JobRequest> = new EventEmitter<JobRequest>();

  constructor(private readonly ngZone: NgZone) { }

  ngOnInit() {

  }

  ngAfterViewInit(): void {
    $(this.modal.nativeElement).on('show.bs.modal', (e: any) => {
      this.ngZone.run(() => {
        this.request = new JobRequest(this.types[0]);
      })
    });
    $(this.modal.nativeElement).on('hide.bs.modal', (e: any) => {
      this.ngZone.run(() => {
      })
    });
  }

  useDefaultTitle(): void {
    if(!this.request) return;
    let t = this.types.find(t => t.type.toString() === this.request.type);
    this.request.title = t.title;
  }

  dismiss() {
    $(this.modal.nativeElement).modal('hide');
  }

  save() {
    this.request.type = parseInt(`${this.request.type}`);
    this.schedule.emit(this.request);
    this.dismiss();
  }

}
