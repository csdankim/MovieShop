import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MovieService } from 'src/app/core/services/movie.service';
import { MovieDetail } from 'src/app/shared/models/movie-detail';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent implements OnInit {

  @Input() movie: MovieDetail | undefined;
  // @Input() movies: MovieDetail[] | undefined;
  @Input() id!: number;
  // @Input() genreId!: number;

  constructor(private movieService: MovieService, private route: ActivatedRoute) { }

  ngOnChanges() {

  }

  // this is where we call our API to get the data
  // reference1: https://www.youtube.com/watch?v=3r43-VITWrU
  // reference2: https://stackoverflow.com/questions/40275862/how-to-get-parameter-on-angular2-route-in-angular-way
  // https://stackoverflow.com/questions/46050849/what-is-the-difference-between-activatedroute-and-activatedroutesnapshot-in-angu
  ngOnInit() {
    console.log('inside ngOnInit method');
    // this.id = +this.route.snapshot.paramMap.getAll('id');
    // this.movieService.getMovieAsync(this.id).subscribe(
    //   m => {
    //     this.movie = m;
    //     console.log(this.movie);
    //   }
    // )
    this.route.paramMap.subscribe(
      params => {
        this.id = +params.getAll('id');   // when get(), it causes Object is possibly 'null' error. why??
        this.movieService.getMovieAsync(this.id).subscribe(
          m => {
            this.movie = m;
            console.log(this.movie);
          }
        )
      }
    );

    // this.route.paramMap.subscribe(
    //   params => {
    //     this.genreId = +params.getAll('id');
    //     this.movieService.getMoviesByGenre(this.genreId).subscribe(g=>{this.movies=g;});
    //   } 
    // );

  }
  ngOnDestroy() {
    console.log('inside ngOnDestroy method');
  }

}
