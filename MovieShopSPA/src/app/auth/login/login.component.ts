import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { Login } from 'src/app/shared/models/login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  // two-way binding
  // one-way binding

  userLogin: Login = {
    email: '', password: ''
  };

  invalidLogin: boolean = false;

  constructor(private authService: AuthenticationService, private route: Router) { }

  ngOnInit(): void {
    // console.log(this.userLogin);
  }

  login() {
    // console.log('form submitted');
    // console.log(f);
    // console.log(this.userLogin);
    this.authService.login(this.userLogin).subscribe(
      (response) => {
        if (response) {
          this.route.navigate(['/'])
        } else {
          this.invalidLogin = true;
        }
      }
    );
  }

  get twowayInfo() {
    return JSON.stringify(this.userLogin);
  }

}
