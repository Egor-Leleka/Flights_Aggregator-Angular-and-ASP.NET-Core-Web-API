import { Injectable } from '@angular/core';
import { FlightRm } from '../../models/flight-rm';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BookDto } from '../../models/book-dto';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FlightService {
  url = "flight";

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
  
    return this.http.get<Array<FlightRm>>(`https://localhost:7290/api/${this.url}`, { params: httpParams });
  }
  
   findFlight(id: string): Observable<FlightRm> {
    return this.http.get<FlightRm>(`https://localhost:7290/api/${this.url}/${id}`);
  }

  bookFlight( booking: BookDto): Observable<BookDto> {
    return this.http.post<BookDto>(`https://localhost:7290/api/${this.url}`, booking);

  }

}
