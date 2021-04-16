import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CastService } from '../core/services/cast.service';
import { CastDetail } from '../shared/models/cast-detail';

@Component({
  selector: 'app-casts',
  templateUrl: './casts.component.html',
  styleUrls: ['./casts.component.css']
})
export class CastsComponent implements OnInit {
  @Input() cast: CastDetail | undefined;
  @Input() id!: number;

  constructor(private castService: CastService, private route: ActivatedRoute) { }

  ngOnInit() {
    console.log('inside ngOnInit method');

    this.route.paramMap.subscribe(
      params => {
        this.id = +params.getAll('id');
        this.castService.getCastWithMovies(this.id).subscribe(
          c => {
            this.cast = c;
            console.log(this.cast);
          }
        )
      }
    );
  }

}
