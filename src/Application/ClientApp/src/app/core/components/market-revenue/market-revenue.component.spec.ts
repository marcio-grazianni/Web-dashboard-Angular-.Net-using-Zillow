import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MarketRevenueComponent } from './market-revenue.component';

describe('MarketRevenueComponent', () => {
  let component: MarketRevenueComponent;
  let fixture: ComponentFixture<MarketRevenueComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MarketRevenueComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MarketRevenueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
