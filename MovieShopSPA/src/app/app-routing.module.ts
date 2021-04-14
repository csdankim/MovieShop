import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MovieCardListComponent } from './movies/movie-card-list/movie-card-list.component';
import { MovieDetailsComponent } from './movies/movie-details/movie-details.component';

const routes: Routes = 
[
  {path: "", component: HomeComponent},
  {path: "genres/movies/:id", component: MovieCardListComponent},
  {path: "movies/:id", component: MovieDetailsComponent}
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
