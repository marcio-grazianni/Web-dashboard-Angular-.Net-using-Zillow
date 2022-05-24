import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MarketYearlyRevenueComponent } from './market-yearly-revenue.component';

describe('MarketYearlyRevenueComponent', () => {
  let component: MarketYearlyRevenueComponent;
  let fixture: ComponentFixture<MarketYearlyRevenueComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MarketYearlyRevenueComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MarketYearlyRevenueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
