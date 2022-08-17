import { Component, OnInit } from '@angular/core';
import { SharedService } from '../shared.service';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms'
import { HttpErrorResponse } from '@angular/common/http';


@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  constructor(private sharedService: SharedService, private router: Router ) { }

  ngOnInit(): void {
  }
  invalidLogin!: boolean;

  RegisterForm = new FormGroup({
    firstName!:	new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]),
    lastName!:	new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]),
    age!: new FormControl(0, [Validators.required, Validators.min(13), Validators.max(150)]),
    gender!: new FormControl('', [Validators.required]),
    emailId!: new FormControl('', [Validators.required, Validators.email, Validators.minLength(9), Validators.maxLength(50)]),
    mobileNumber!: new FormControl('', [Validators.required , Validators.minLength(10), Validators.maxLength(10)]),
    username!: new FormControl('', [Validators.required, Validators.minLength(6), Validators.maxLength(15)]),
    password!: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(16)])
  })
  
  customer: any = null;
  
  RegisterCustomer(){
    console.log(this.RegisterForm.value);
    this.sharedService.RegisterCustomer(this.RegisterForm.value)
                      .subscribe(
                        data =>
                        {
                          this.customer = data;
                          this.invalidLogin = false;
                          alert("You have registered successfully");
                          this.router.navigate(["login"]);
                        },
                        err=>{
                          alert("Something Went Wrong" + err );
                          console.log(err);
                          //this.router.navigate(["changepassword"]);
                        });                      
  }

  get f(){
    return this.RegisterForm.controls;
  }
}
