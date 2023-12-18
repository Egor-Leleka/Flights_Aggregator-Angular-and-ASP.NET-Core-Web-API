import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SearchFlightsComponent } from './components/search-flights/search-flights.component';
import { BookFlightComponent } from './components/book-flight/book-flight.component';

const routes: Routes = [
  {
    path: 'search-flights',
    component: SearchFlightsComponent
  },
  {
    path: 'book-flight/:flightId',
    component: BookFlightComponent
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
