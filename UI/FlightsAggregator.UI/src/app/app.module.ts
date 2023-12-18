import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { SearchFlightsComponent } from './components/search-flights/search-flights.component';
import {HttpClientModule} from '@angular/common/http'
import { ReactiveFormsModule } from '@angular/forms';
import { BookFlightComponent } from './components/book-flight/book-flight.component';
import { RegisterPassengerComponent } from './components/register-passenger/register-passenger.component';
import { MyBookingComponent } from './components/my-booking/my-booking.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    SearchFlightsComponent,
    BookFlightComponent,
    RegisterPassengerComponent,
    MyBookingComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
