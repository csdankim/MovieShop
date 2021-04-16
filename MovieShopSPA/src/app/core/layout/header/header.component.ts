import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/shared/models/user';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {


  isAuthenticated: boolean | undefined;
  currentUser: User | undefined;

  constructor(private authService: AuthenticationService) { }

  ngOnInit(): void {

    this.authService.isAuthenticated.subscribe(
       auth => {
         this.isAuthenticated = auth;
         console.log('Auth Status: '+ this.isAuthenticated)
       }
    );
  }

}
