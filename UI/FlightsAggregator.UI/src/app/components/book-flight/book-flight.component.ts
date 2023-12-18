import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BookDto } from 'src/app/models/book-dto';
import { FlightService } from 'src/app/services/flight/flight.service';
import { AuthService } from '../../services/auth/auth.service';
import { FlightRm } from 'src/app/models/flight-rm';

@Component({
  selector: 'app-book-flight',
  templateUrl: './book-flight.component.html',
  styleUrls: ['./book-flight.component.css']
})
export class BookFlightComponent implements OnInit {

  constructor(private route: ActivatedRoute,
    private router: Router,
    private flightService: FlightService,
    private authService: AuthService,
    private fb: FormBuilder  ){}

  flightId: string = 'not loaded'
  flight: FlightRm = {}

  form = this.fb.group({
    number: [1, Validators.compose([Validators.required, Validators.min(1), Validators.max(254)])]
  })


  ngOnInit(): void {
    this.route.paramMap
  .subscribe(p => this.findFlight(p.get("flightId")));
  }

  private findFlight(flightId: string | null) {
    if (flightId) {
      this.flightService.findFlight(flightId)
        .subscribe(
          flight => this.flight = flight,
          err => this.handleError(err)
        );
    } else {
      this.flightId = 'not passed';
    }
  }
  

  private handleError = (err: any) => {

    if (err.status == 404) {
      alert("Flight not found!")
      this.router.navigate(['/search-flights'])
    }
  
  
    if (err.status == 409) {
      console.log("err: " + err);
      alert(JSON.parse(err.error).message)
    }
  }

  

  book() {

    if (this.form.invalid)
      return;

      console.log(`Booking ${this.form.get('number')?.value} passengers for the flight: ${this.flight.id}`)
  
    const booking: BookDto = {
      flightId: this.flight.id,
      numberOfSeats: this.form.get('number')?.value!,
      passengerEmail: this.authService.currentUser?.email
    }
  
    this.flightService.bookFlight( booking )
      .subscribe(_ => this.router.navigate(['/my-booking']),
        this.handleError)
  
  }

  get number() {
    return this.form.controls.number
  }
}
