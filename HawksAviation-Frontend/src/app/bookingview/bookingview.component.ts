import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { jsPDF } from 'jspdf';
import { Bookings } from '../Models/bookings.model';
import { SharedService } from '../shared.service';
import html2canvas from 'html2canvas';

@Component({
  selector: 'app-bookingview',
  templateUrl: './bookingview.component.html',
  styleUrls: ['./bookingview.component.css']
})
export class BookingviewComponent implements OnInit {

  constructor(private router: Router, private jwtHelper: JwtHelperService, private sharedService: SharedService) { }

  booking!: Bookings; 
  token : any = "bearer "+ localStorage.getItem("jwt");

  decodedToken = this.jwtHelper.decodeToken(this.token);

  user:any = {
  Email: '',
  FName: '',
  LName: '',
  Role: '',
  UserId:''
  }

  customer:any;
  flight: any;

  GetCustomers(Id: any)
  {
    console.log(Id)
  this.sharedService.getCustomerById(Number(Id)).subscribe(data =>
  {
    this.customer = data;
    console.log("ct"+this.customer);
  });
  }
  public Fairport: any = {
    airportID: '',
    name: '',
    city:'',
    country:''
  };

  public Dairport: any = {
    airportID: '',
    name: '',
    city:'',
    country:''
  };

  GetFlight(Id: any)
  {
    console.log(Id)
  this.sharedService.getFlightById(Number(Id)).subscribe(data =>
  {
    this.flight = data;
    console.log("fl"+this.flight);
    
  });
  this.sharedService.getAirportById(this.flight.start).subscribe(data =>
    {
      this.Fairport = data;
      console.log("F"+this.Fairport.city)
    });
    this.sharedService.getAirportById(this.flight.destination).subscribe(data =>
      {
        this.Dairport = data;
        console.log("T"+this.Dairport.city)
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

    this.booking = history.state;
    this.GetCustomers(this.booking.customerID);
    this.GetFlight(this.booking.flightNo);
  }

  public openPDF(): void {
    let DATA: any = document.getElementById('htmlData');
    html2canvas(DATA).then((canvas) => {
      let fileWidth = 208;
      let fileHeight = (canvas.height * fileWidth) / canvas.width;
      const FILEURI = canvas.toDataURL('image/png');
      let PDF = new jsPDF('p', 'mm', 'a4');
      let position = 0;
      PDF.addImage(FILEURI, 'PNG', 0, position, fileWidth, fileHeight);
      PDF.save('Booking.pdf');
    });
  }

}

