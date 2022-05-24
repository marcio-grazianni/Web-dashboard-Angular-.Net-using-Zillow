import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListingSearchComponent } from './listing-search.component';

describe('ListingSearchComponent', () => {
  let component: ListingSearchComponent;
  let fixture: ComponentFixture<ListingSearchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListingSearchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListingSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
