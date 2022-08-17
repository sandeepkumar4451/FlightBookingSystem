import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookaticketComponent } from './bookaticket.component';

describe('BookaticketComponent', () => {
  let component: BookaticketComponent;
  let fixture: ComponentFixture<BookaticketComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BookaticketComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BookaticketComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
