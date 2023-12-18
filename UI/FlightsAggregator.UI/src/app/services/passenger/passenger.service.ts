import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { NewPassengerDto } from 'src/app/models/new-passenger-dto';
import { PassengerRm } from 'src/app/models/passenger-rm';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PassengerService {
  url = "passenger";

  constructor(private http: HttpClient) { }

  findPassenger(email: string): Observable<PassengerRm>{
    return this.http.get<PassengerRm>(`https://localhost:7290/api/${this.url}/${email}`);
  }


    registerPassenger(newPassenger: NewPassengerDto): Observable<NewPassengerDto>{
      return this.http.post<NewPassengerDto>(`https://localhost:7290/api/${this.url}`, newPassenger);
    }
    
}
