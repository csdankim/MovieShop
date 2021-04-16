import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CastDetail } from 'src/app/shared/models/cast-detail';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class CastService {

  constructor(private apiService: ApiService) { }

  getCastWithMovies(id: number): Observable<CastDetail> {
    return this.apiService.getById(`${'casts'}`, id);
  }
}
