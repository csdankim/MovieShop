import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MovieCard } from 'src/app/shared/models/movie-card';
import { MovieDetail } from 'src/app/shared/models/movie-detail';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  constructor(private apiService: ApiService) { }

  getTop30GrossingMovies(): Observable<MovieCard[]> {

    return this.apiService.getAll('movies/toprevenue');

  }

  // reference1: https://www.youtube.com/watch?v=3r43-VITWrU
  // reference2: https://stackoverflow.com/questions/40275862/how-to-get-parameter-on-angular2-route-in-angular-way
  getMovieAsync(id: number): Observable<MovieDetail> {

    return this.apiService.getById(`${'movies'}`, id);

  }

}