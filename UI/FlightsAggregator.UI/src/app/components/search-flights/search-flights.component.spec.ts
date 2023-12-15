import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchFlightsComponent } from './search-flights.component';

describe('SearchFlightsComponent', () => {
  let component: SearchFlightsComponent;
  let fixture: ComponentFixture<SearchFlightsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SearchFlightsComponent]
    });
    fixture = TestBed.createComponent(SearchFlightsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
