import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Flights } from '../Models/flights.model';
import { SharedService } from '../shared.service';

@Component({
  selector: 'app-flightslist',
  templateUrl: './flightslist.component.html',
  styleUrls: ['./flightslist.component.css']
})
export class FlightslistComponent implements OnInit {

  constructor(private router: Router, private jwtHelper: JwtHelperService, private sharedService: SharedService) { }
  token : any = "bearer "+ localStorage.getItem("jwt");

  decodedToken = this.jwtHelper.decodeToken(this.token);

  formValue !: FormGroup;
  flightModelObj : Flights = new Flights();
  flightData !: any;

  airports:any = {
    start!: '',
    destination!: '',
    doj: Date
  };

  user:any = {
    Email: '',
    FName: '',
    LName: '',
    Role: '',
    UserId:''
    }
  flightList: Flights[] = [];

  flightS!: Flights;

  GetflightList(start:any, destination:any, doj:any) {
    this.sharedService.searchflights(start, destination, doj)
    .subscribe(data =>{
      console.log(data);
      this.flightList = data;
    }); 
  }

  ngOnInit(): void {
    this.token = "bearer "+ localStorage.getItem("jwt");
    this.decodedToken = this.jwtHelper.decodeToken(this.token);
    this.user = {
      Email: this.decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'],
      FName: this.decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
      LName: this.decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname'],
      Role: this.decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'],
      UserId: this.decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber']
    }

    this.airports=history.state;
    console.log(this.airports);
    this.GetflightList(this.airports.start, this.airports.destination, this.airports.doj)
  }

  OnclickBook(flight: any){
    this.flightS = flight;
    this.router.navigateByUrl('/newbooking', { state:  this.flightS});
  }

  
}
