import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Login } from 'src/app/shared/models/login';
import { ApiService } from './api.service';
import { JwtStorageService } from './jwt-storage.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private apiService: ApiService, private jwtStorageService: JwtStorageService) { }

  login(userLogin: Login): Observable<boolean> {
    //take un/pw from login component and post it to API
    // once API returns token. we need to store the token in localstorage of the browser. 
    // otherwise return false to component to that component can show the message in the UI
    return this.apiService.create('account/login', userLogin).pipe( map ( response=>{
      if (response) {
        // save the response token to localStorage
        console.log(response);
        this.jwtStorageService.saveToken(response.token)
        return true;
      }
      return false;
    }));
  }

  logout() {
    // we remove the token from local storage
    this.jwtStorageService.destroyToken();
  }

  decodeToken() {
    // it will read the token from localstorage and decode it and put it in User object
  }
}
