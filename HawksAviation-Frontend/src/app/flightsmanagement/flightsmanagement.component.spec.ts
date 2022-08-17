import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlightsmanagementComponent } from './flightsmanagement.component';

describe('FlightsmanagementComponent', () => {
  let component: FlightsmanagementComponent;
  let fixture: ComponentFixture<FlightsmanagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlightsmanagementComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FlightsmanagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
