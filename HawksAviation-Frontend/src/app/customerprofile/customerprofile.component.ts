import { Component, OnInit } from '@angular/core';
import { EmailValidator } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Bookings } from '../Models/bookings.model';
import { Customers } from '../Models/customers.model';
import { SharedService } from '../shared.service';

@Component({
  selector: 'app-customerprofile',
  templateUrl: './customerprofile.component.html',
  styleUrls: ['./customerprofile.component.css']
})
export class CustomerprofileComponent implements OnInit {

  constructor(private router: Router, private jwtHelper: JwtHelperService, private sharedService: SharedService) { }

  customer: any;

  IsMale!: boolean;

  token : any = "bearer "+ localStorage.getItem("jwt");

  decodedToken = this.jwtHelper.decodeToken(this.token);

  user:any = {
  Email: '',
  FName: '',
  LName: '',
  Role: '',
  UserId:''
  }

  bookings: Bookings[] = [];

  GetCustomers(Id: any)
  {
    console.log(Id)
  this.sharedService.getCustomerById(Number(this.user.UserId)).subscribe(data =>
  {
    this.customer = data;
    this.IsMale = this.customer['gender'] == 'Male';
    console.log(this.customer);
  });
  }

  GetCustBookings(Id: any)
  {
    console.log(Id)
  this.sharedService.getCustomerBookings(Number(this.user.UserId)).subscribe(data =>
  {
    this.bookings = data;
    console.log(this.customer);
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
    if (this.user.Role != 'Customer')
    {
      this.router.navigate(["401unauthorizedaccess"]);
    }
    this.GetCustomers(this.user.UserId)
    this.GetCustBookings(this.user.UserId)
  }

  
  deleteBookings(row:any){
    this.sharedService.deleteBookings(row.bookingID)
    .subscribe(res=>{
      alert("Reservation Cancelled Successfully!!!!!");
      this.GetCustBookings(this.user.UserId)
    })
  }
  updateBookings(row:any){
    this.sharedService.updateBookings(row.bookingID)
    .subscribe(res=>{
      alert("CheckedIn Successfully!!!!!!!!!!!");
      this.GetCustBookings(this.user.UserId)
    })
  }

}
