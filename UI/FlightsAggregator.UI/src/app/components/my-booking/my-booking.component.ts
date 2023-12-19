import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BookDto } from 'src/app/models/book-dto';
import { BookingRm } from 'src/app/models/booking-rm';
import { AuthService } from 'src/app/services/auth/auth.service';
import { BookingService } from 'src/app/services/booking/booking.service';

@Component({
  selector: 'app-my-booking',
  templateUrl: './my-booking.component.html',
  styleUrls: ['./my-booking.component.css']
})
export class MyBookingComponent implements OnInit {

  bookings!: BookingRm[];
  email: string = this.authService.currentUser?.email ?? '';
 
  constructor(private bookingService: BookingService,
    private authService: AuthService,
    private router: Router) { }


  ngOnInit(): void {
    if(!this.email)
      this.router.navigate(['/register-passenger']);

    this.bookingService.listBooking(this.email)
      .subscribe(
        r => this.bookings = r, 
        err => this.handleError(err)
      );
  }

  private handleError(err: any) {
    console.log("Response Error, Status:", err.status);
    console.log("Response Error, Status Text:", err.statusText);
    console.log(err);
  }

  cancel(booking: BookingRm) {

    const dto: BookDto = {
      flightId: booking.flightId,
      numberOfSeats: booking.numberOfBookedSeats,
      passengerEmail: booking.passengerEmail
    };

    this.bookingService.cancelBooking(dto)
      .subscribe(_ =>
        this.bookings = this.bookings.filter(b => b != booking)
        ,this.handleError);
  }
}