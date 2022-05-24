import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MarketSummaryComponent } from './market-summary.component';

describe('MarketSummaryComponent', () => {
  let component: MarketSummaryComponent;
  let fixture: ComponentFixture<MarketSummaryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MarketSummaryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MarketSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
