import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthenticatedResponse } from '../_interfaces/authenticated-response';
import { Login } from '../_interfaces/login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  invalidLogin!: boolean;
  credentials: Login = {username:'', password:''};
  decodedToken :any;

  user:any = {
  Email: '',
  FName: '',
  LName: '',
  Role: '',
  UserId:''
  }

  constructor(private router: Router, private http: HttpClient,private jwtHelper: JwtHelperService) { }

  ngOnInit(): void {
  }

  login = ( form: NgForm) => {
    if (form.valid) {
      this.http.post<AuthenticatedResponse>("https://localhost:7178/api/auth/login", this.credentials, {
        headers: new HttpHeaders({ "Content-Type": "application/json"})
      })
      .subscribe({
        next: (response: AuthenticatedResponse) => {
          const token = response.token;
          localStorage.setItem("jwt", token);
          this.decodedToken = this.jwtHelper.decodeToken(token);
          this.user = {
            Email: this.decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'],
            FName: this.decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
            LName: this.decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname'],
            Role: this.decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'],
            UserId: this.decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber']
          }
          this.invalidLogin = false;
          alert(this.user.Role + " login Successful!!, Welcome " + this.user.FName);
          this.router.navigate(["home"]);
        },
        error: (err: HttpErrorResponse) => this.invalidLogin = true
      })
    }
  }

}

