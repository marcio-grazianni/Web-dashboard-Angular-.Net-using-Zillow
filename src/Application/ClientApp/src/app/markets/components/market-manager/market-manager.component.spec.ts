import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MarketManagerComponent } from './market-manager.component';

describe('MarketManagerComponent', () => {
  let component: MarketManagerComponent;
  let fixture: ComponentFixture<MarketManagerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MarketManagerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MarketManagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
