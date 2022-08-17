import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SharedService } from '../shared.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  constructor(private sharedService: SharedService, private router: Router ) { }

  ngOnInit(): void {
  }
  invalidLogin!: boolean;

  RegisterForm = new FormGroup({
    firstName!:	new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]),
    lastName!:	new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]),
    emailId!: new FormControl('', [Validators.required, Validators.email, Validators.minLength(9), Validators.maxLength(50)]),
    role!: new FormControl('', [Validators.required , Validators.minLength(5), Validators.maxLength(20)]),
    username!: new FormControl('', [Validators.required, Validators.minLength(6), Validators.maxLength(15)]),
    password!: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(16)])
  })
  
  admin: any = null;
  
  RegisterAdmin(){
    console.log(this.RegisterForm.value)
    this.sharedService.RegisterAdmin(this.RegisterForm.value)
                      .subscribe(
                        data =>
                        {
                          console.log(data)
                          this.admin = data;
                          this.invalidLogin = false;
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
