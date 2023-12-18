import { Injectable } from '@angular/core';
import { FlightRm } from '../models/flight-rm';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BookDto } from '../models/book-dto';

@Injectable({
  providedIn: 'root'
})
export class FlightService {

  constructor(private http: HttpClient) { }

  getFlightsFromApi(params?: {
    fromDate?: string;
    toDate?: string;
    from?: string;
    destination?: string;
    numberOfPassengers?: number;
  }): Observable<Array<FlightRm>> {
    let httpParams = new HttpParams();
  
    if (params) {
      Object.keys(params).forEach(key => {
        const value = params[key as keyof typeof params];
        if (value !== undefined) {
          httpParams = httpParams.set(key, value.toString());
        }
      });
    }
  
    return this.http.get<Array<FlightRm>>(`https://localhost:7290/api/flight`, { params: httpParams });
  }

  
   findFlight(id: string): Observable<FlightRm> {
    return this.http.get<FlightRm>(`https://localhost:7290/api/flight/${id}`);
  }
  

  // bookFlight(params?: {
  //   body?: BookDto
  // }): Observable<void> {
  //   return

  // }

  getMockFlights(): FlightRm[] {
    const mockFlights: FlightRm[] = [];
    // Generate mock flight data
    for (let i = 0; i < 10; i++) {
      mockFlights.push({
        airline: `Airline ${i}`,
        arrival: {
          place: `Destination ${i}`,
          time: new Date().toISOString()
        },
        departure: {
          place: `Origin ${i}`,
          time: new Date().toISOString()
        },
        // id: `flight-${i}`,
        price: `${100 + i * 10} USD`,
        remainingNumberOfSeats: 20 - i
      });
    }
    return mockFlights;
  }
}
