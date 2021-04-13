import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Genre } from 'src/app/shared/models/genre';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class GenreService {

  constructor(private apiService: ApiService) { }

  getAllGenres(): Observable<Genre[]> {
    // make a call to API to get JSON data and wrap it in Genre array and return
    // we call our base ApiService which is gonna call our API using HttpClient class
    return this.apiService.getAll('Genres');
  }
}
