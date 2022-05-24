import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MarketCodeFinderComponent } from './market-code-finder.component';

describe('MarketCodeFinderComponent', () => {
  let component: MarketCodeFinderComponent;
  let fixture: ComponentFixture<MarketCodeFinderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MarketCodeFinderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MarketCodeFinderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
