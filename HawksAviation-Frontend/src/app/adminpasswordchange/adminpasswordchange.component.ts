import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CustomValidators } from '../CustomValidators.validator';
import { SharedService } from '../shared.service';

@Component({
  selector: 'app-adminpasswordchange',
  templateUrl: './adminpasswordchange.component.html',
  styleUrls: ['./adminpasswordchange.component.css']
})
export class AdminpasswordchangeComponent implements OnInit {

  constructor(private sharedService: SharedService,private router: Router) { }

  ngOnInit(): void {
  }

  PasswordChangeForm = new FormGroup({
    username: new FormControl('',[Validators.required, Validators.minLength(6), Validators.maxLength(15)]),
    oldPassword: new FormControl('',[Validators.required, Validators.minLength(3), Validators.maxLength(16)]),
    newPassword: new FormControl('',[Validators.required, Validators.minLength(3), Validators.maxLength(16)]),
    confirmPassword: new FormControl('', [Validators.required])
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
    this.sharedService.ChangePasswordAdmin(this.creds)
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
