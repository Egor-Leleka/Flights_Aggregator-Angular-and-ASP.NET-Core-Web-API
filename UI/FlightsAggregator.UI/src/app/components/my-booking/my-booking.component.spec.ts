import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyBookingComponent } from './my-booking.component';

describe('MyBookingComponent', () => {
  let component: MyBookingComponent;
  let fixture: ComponentFixture<MyBookingComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MyBookingComponent]
    });
    fixture = TestBed.createComponent(MyBookingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
