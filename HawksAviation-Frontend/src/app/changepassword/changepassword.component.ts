import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CustomValidators } from '../CustomValidators.validator';
import { SharedService } from '../shared.service';
import { Login } from '../_interfaces/login';

@Component({
  selector: 'app-changepassword',
  templateUrl: './changepassword.component.html',
  styleUrls: ['./changepassword.component.css']
})
export class ChangepasswordComponent implements OnInit {

  constructor(private sharedService: SharedService,private router: Router) { }

  ngOnInit(): void {
  }

  PasswordChangeForm = new FormGroup({
    username: new FormControl('', [Validators.required]),
    oldPassword: new FormControl('', [Validators.required]),
    newPassword: new FormControl('', [Validators.required]),
    confirmPassword: new FormControl('', [Validators.required]),
  },
  [CustomValidators.MatchValidator('newPassword', 'confirmPassword')]);

  creds:any = {
    username : '',
    oldPassword : '',
    newPassword : '',
  };

  ChangePassword(){ 
    console.log(this.PasswordChangeForm.value)
    this.creds.username = this.PasswordChangeForm.controls['username'].value;
    this.creds.oldPassword= this.PasswordChangeForm.controls['oldPassword'].value;
    this.creds.newPassword = this.PasswordChangeForm.controls['newPassword'].value;
    this.sharedService.ChangePasswordCustomer(this.creds)
                      .subscribe(data =>
                        {
                          this.creds = data;
                          alert("Password changed Successfully")
                          this.router.navigate(["login"]);
                        },
                        err=>{
                          alert("Something Went Wrong" + err );
                          //this.router.navigate(["changepassword"]);
                        });
  }

  get passwordMatchError() {
    return (
      this.PasswordChangeForm.getError('mismatch') &&
      this.PasswordChangeForm.get('confirmPassword')?.touched
    );
  }
}
