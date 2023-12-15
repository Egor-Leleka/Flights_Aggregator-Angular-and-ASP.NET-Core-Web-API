import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SearchFlightsComponent } from './components/search-flights/search-flights.component';

const routes: Routes = [
  {
    path: 'search-flights',
    component: SearchFlightsComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
