import { Component, OnInit } from '@angular/core';
import { GenreService } from '../core/services/genre.service';
import { Genre } from '../shared/models/genre';

@Component({
  selector: 'app-genres',
  templateUrl: './genres.component.html',
  styleUrls: ['./genres.component.css']
})
export class GenresComponent implements OnInit {

  // this property will be available to view so that it can use to display data
  // genres: Genre[] = [];
  genres: Genre[] | undefined;

  constructor(private genreService: GenreService) { }

  ngOnChanges() {

  }

  // this is where we call our API to get the data
  ngOnInit() {
    console.log('inside ngOnInit method');
    this.genreService.getAllGenres().subscribe(
      g=>{
        this.genres=g;
        // console.log('genres');
        // console.log(this.genres);
        console.table(this.genres);
      }
    )
  }
  ngOnDestroy(){
    console.log('inside ngOnDestroy method');
  }


}
