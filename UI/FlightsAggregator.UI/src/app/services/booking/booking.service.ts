import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BookDto } from 'src/app/models/book-dto';
import { BookingRm } from 'src/app/models/booking-rm';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  url = "booking"

  constructor(private http: HttpClient) { }

  cancelBooking(booking: BookDto): Observable<void>{
    return this.http.delete<void>(`https://localhost:7290/api/${this.url}`)
  }

  listBooking(email: string): Observable<Array<BookingRm>>{
    return this.http.get<Array<BookingRm>>(`https://localhost:7290/api/${email}`)
  }
}
