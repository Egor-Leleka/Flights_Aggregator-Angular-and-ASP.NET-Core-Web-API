import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { FlightRm } from 'src/app/models/flight-rm';
import { FlightService } from 'src/app/services/flight/flight.service';

@Component({
  selector: 'app-search-flights',
  templateUrl: './search-flights.component.html',
  styleUrls: ['./search-flights.component.css']
})
export class SearchFlightsComponent implements OnInit {

  constructor(private fs: FlightService,
    private fb: FormBuilder) { }

  searchResult: FlightRm[] = []

  searchForm = this.fb.group({
    from: [''],
    destination: [''],
    fromDate: [''],
    toDate: [''],
    numberOfPassengers: [1]
  });


  ngOnInit(): void {
    this.search();
    // this.searchResult = this.fs.getMockFlights();
  }

  search() {

    const searchParams = {
      from: this.searchForm.value.from || undefined,
      destination: this.searchForm.value.destination || undefined,
      fromDate: this.searchForm.value.fromDate || undefined,
      toDate: this.searchForm.value.toDate || undefined,
      numberOfPassengers: this.searchForm.value.numberOfPassengers || undefined
    };
  
    this.fs.getFlightsFromApi(searchParams)
      .subscribe(
        response => this.searchResult = response,
        err => this.handleError(err)
      );
  }

  private handleError(err: any) {
    console.log("Response Error. Status: ", err.status)
    console.log("Response Error. Status Text: ", err.statusText)
    console.log(err)
  }
}
