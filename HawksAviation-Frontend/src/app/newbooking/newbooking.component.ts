import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Flights } from '../Models/flights.model';
import { SharedService } from '../shared.service';

@Component({
  selector: 'app-newbooking',
  templateUrl: './newbooking.component.html',
  styleUrls: ['./newbooking.component.css']
})
export class NewbookingComponent implements OnInit {

  constructor(private router: Router, private jwtHelper: JwtHelperService, private sharedService: SharedService) { }
  token : any = "bearer "+ localStorage.getItem("jwt");

  decodedToken = this.jwtHelper.decodeToken(this.token);

  user:any = {
  Email: '',
  FName: '',
  LName: '',
  Role: '',
  UserId:''
  }

  flightNo!: Number;
  customerID!: Number;
  seats!: Number;
  ASeats!: Number;
  booking:any;
  InvSeats:boolean = false;

  bookingcdto: any = {
    flightNo: Number,
    customerID: Number,
    seats: Number
  }


  flightS!: Flights;

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

    this.flightS=history.state;
    this.flightNo = this.flightS.flightNo;
    this.customerID = this.user.UserId;
    this.ASeats = this.flightS.availableSeats;
    console.log(this.customerID);
  }

  newBooking(flightNo:any, customerID:any, seats:any) {
    this.bookingcdto.flightNo = flightNo;
    this.bookingcdto.customerID = customerID;
    this.bookingcdto.seats = Math.abs(seats);
    console.log(this.bookingcdto.seats);
    let response = confirm("Payment Interface: This is a payment confirmation. If you wish to book the flight ticket successfully, press click the 'OK' button or else if you wish not to continue to booking the ticket, please click 'Cancel' button. So do you wish to finish booking the flight ticket?........")
    if (response)
      this.sharedService.NewBooking(this.bookingcdto).subscribe(data =>{
      this.booking = data;
      console.log(data);
      alert("Flight ticket Booked successfully and redirecting to the booking view page........................." );
      this.router.navigateByUrl('/bookingview', { state:  this.booking,});
    }); 

  }

  checkseatsAvailable(seats: any)
  {
    console.log(seats)
    this.InvSeats = Number(seats) > this.ASeats || Number(seats)<= 0;
    console.log(this.InvSeats)
    return this.InvSeats
  }

}
