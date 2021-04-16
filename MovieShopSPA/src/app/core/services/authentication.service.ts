import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Login } from 'src/app/shared/models/login';
import { User } from 'src/app/shared/models/user';
import { ApiService } from './api.service';
import { JwtStorageService } from './jwt-storage.service';
import { JwtHelperService } from "@auth0/angular-jwt";

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {


  private user!: User | null;

  private currentUserSubject = new BehaviorSubject<User>({} as User);
  public currentUser = this.currentUserSubject.asObservable();

  private isAuthenticatedSubject = new BehaviorSubject<boolean>(false);
  public isAuthenticated = this.isAuthenticatedSubject.asObservable();

  constructor(private apiService: ApiService, private jwtStorageService: JwtStorageService) { }

  login(userLogin: Login): Observable<boolean> {
    //take un/pw from login component and post it to API
    // once API returns token. we need to store the token in localstorage of the browser. 
    // otherwise return false to component to that component can show the message in the UI
    return this.apiService.create('account/login', userLogin).pipe(map(response => {
      if (response) {
        // save the response token to localStorage
        console.log(response);
        this.jwtStorageService.saveToken(response.token);
        this.populateUserInfo();
        return true;
      }
      return false;
    }));
  }

  logout() {
    // we remove the token from local storage
    this.jwtStorageService.destroyToken();

    // setting default values to observables
    this.currentUserSubject.next({} as User);
    this.isAuthenticatedSubject.next(false);
  }

  decodeToken(): User | null {
    // it will read the token from localstorage and decode it and put it in User object
    // also check the token is not expired
    const token = this.jwtStorageService.getToken();

    if(token !=null) {
      const tokenExpired = new JwtHelperService().isTokenExpired(token);
      if (tokenExpired || !token)
        return null;
  
      const decodedToken = new JwtHelperService().decodeToken(token);
  
      this.user = decodedToken;
      return this.user;
    }
    
    return null;
  }

  populateUserInfo() {

    if (this.jwtStorageService.getToken()) {

      const decodedToken = this.decodeToken();

      if (decodedToken != null) {
        this.currentUserSubject.next(decodedToken);
      }

      this.isAuthenticatedSubject.next(true);
    }

  }
}
