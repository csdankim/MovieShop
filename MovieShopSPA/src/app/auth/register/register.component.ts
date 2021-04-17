import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  submitted = false;
  registerForm!: FormGroup;

  constructor(private fb: FormBuilder) { }

  get f() {
    return this.registerForm.controls;
  }

  buildForm() {
    this.registerForm=this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email:[null, {validators: [Validators.required, Validators.email]}],
      password:['', Validators.required],
      confirmPassword:['', Validators.required]
    })
  }

  ngOnInit() {
    this.buildForm();
  }

  onSubmit() {
    console.log(this.registerForm);
    this.submitted = true;
    if (this.registerForm.invalid) {
      return;
    }
  }

}
