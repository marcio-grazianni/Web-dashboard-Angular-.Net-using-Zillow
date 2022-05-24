import { Component, OnInit } from '@angular/core';
import { ListingService } from 'src/app/api/services/listing.service';
import { Listing } from 'src/app/api/types/listing';

@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.css']
})
export class FavoritesComponent implements OnInit {

  listings: Listing[] = [];
  constructor(private readonly listingService: ListingService) { }

  ngOnInit() {
    this.load();
  }

  async load(): Promise<void> {
    this.listings = await this.listingService
      .getFavorites()
      .toPromise();
  }
}
