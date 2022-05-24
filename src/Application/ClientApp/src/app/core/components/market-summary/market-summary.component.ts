import { Component, Input, OnInit } from '@angular/core';
import { MarketHistory } from 'src/app/api/types/market-history';

@Component({
  selector: 'market-summary',
  templateUrl: './market-summary.component.html',
  styleUrls: ['./market-summary.component.css']
})
export class MarketSummaryComponent implements OnInit {

  summary: any = null;
  @Input() set history(v: MarketHistory) {
    if (v) {
      this.summary = v.getLatestSummary();
      this.resolveStrings();
    }
  }

  get hasDataAvailable(): boolean {
    return this.summary !== null;
  }

  get totalProperties(): number {
    return this.hostInfo.host_properties.total_properties;
  }
  get singleHostProperties(): number {
    return this.hostInfo.host_properties.single_host_properties;
  }
  get multiHostProperties(): number {
    return this.hostInfo.host_properties.multi_host_properties;
  }

  get totalHosts(): number {
    return this.hostInfo.hosts.total_hosts;
  }
  get superHosts(): number {
    return this.hostInfo.hosts.superhosts;
  }
  get multiUnitHosts(): number {
    return this.hostInfo.hosts.multi_unit_hosts;
  }
  get singleUnitHosts(): number {
    return this.hostInfo.hosts.single_unit_hosts;
  }

  constructor() { }

  ngOnInit() {
  }

  period: string = '';
  roomTypes: any[] = [];
  hostInfo: any;
  private resolveStrings(): void {
    if (!this.summary) return;
    console.log(this.summary);
    let calendarMonths = this.summary.calendar_months;
    
    let rentalActivity = this.summary.rental_activity;
    let rentalCounts = this.summary.rental_counts;

    

    let roomTypes = [];
    for(let property in calendarMonths.room_type) {
      let type = calendarMonths.room_type[property];
      roomTypes.push({
        type: property,
        adr : type.adr['50th_percentile'],
        occupancy : type.occ['50th_percentile'] * 100,
        revenue : type.revenue['50th_percentile']
      });
    }
    this.period = `(${calendarMonths.month}/${calendarMonths.year})`;
    this.roomTypes = roomTypes;
    this.hostInfo = this.summary.host_info;
  }

}
