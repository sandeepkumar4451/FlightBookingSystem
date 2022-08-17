import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminpasswordchangeComponent } from './adminpasswordchange.component';

describe('AdminpasswordchangeComponent', () => {
  let component: AdminpasswordchangeComponent;
  let fixture: ComponentFixture<AdminpasswordchangeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminpasswordchangeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminpasswordchangeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
