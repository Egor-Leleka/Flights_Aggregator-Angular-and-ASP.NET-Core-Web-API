import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BookDto } from 'src/app/models/book-dto';
import { BookingRm } from 'src/app/models/booking-rm';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  private apiUrl = `https://localhost:7290/api/booking`;

  constructor(private http: HttpClient) { }

  listBooking(email: string): Observable<Array<BookingRm>>{
    return this.http.get<Array<BookingRm>>(`${this.apiUrl}/${email}`);
  }

  cancelBooking(booking: BookDto): Observable<void> {
    const httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json'
        }),
        body: booking
    };
    return this.http.delete<void>(`${this.apiUrl}`, httpOptions);
}
}
