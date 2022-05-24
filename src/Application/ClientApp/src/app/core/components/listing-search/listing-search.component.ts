import { Component, OnInit } from '@angular/core';
import { ListingService } from 'src/app/api/services/listing.service';

@Component({
  selector: 'search',
  templateUrl: './listing-search.component.html',
  styleUrls: ['./listing-search.component.css']
})
export class ListingSearchComponent implements OnInit {

  items: any[] = [];
  query: string = '';
  constructor(private readonly listingService: ListingService) { }

  ngOnInit() {
  }

  async search(): Promise<void> {
    let cond = this.query && this.query.length > 3;
    this.items = cond ? await this.listingService
      .search(this.query)
      .toPromise()
      : [];
  }

}
