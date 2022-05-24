import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApifyComponent } from './apify.component';

describe('ApifyComponent', () => {
  let component: ApifyComponent;
  let fixture: ComponentFixture<ApifyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApifyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApifyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
