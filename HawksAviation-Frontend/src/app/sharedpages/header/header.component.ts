import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(private router: Router, private jwtHelper: JwtHelperService) { }

  ngOnInit(): void {
  }

  isUserAuthenticated = (): boolean => {
    const token = localStorage.getItem("jwt");

    if (token && !this.jwtHelper.isTokenExpired(token)){
      return true;
    }

    return false;
  }

  logOut = () => {
    localStorage.removeItem("jwt");
    this.router.navigate(["home"]);
  }

}
