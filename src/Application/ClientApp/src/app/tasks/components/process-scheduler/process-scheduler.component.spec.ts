import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProcessSchedulerComponent } from './process-scheduler.component';

describe('ProcessSchedulerComponent', () => {
  let component: ProcessSchedulerComponent;
  let fixture: ComponentFixture<ProcessSchedulerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProcessSchedulerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProcessSchedulerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
