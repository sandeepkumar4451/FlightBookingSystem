import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Bookings } from '../Models/bookings.model';
import { SharedService } from '../shared.service';

@Component({
  selector: 'app-bookingslist',
  templateUrl: './bookingslist.component.html',
  styleUrls: ['./bookingslist.component.css']
})
export class BookingslistComponent implements OnInit {

  formValue !: FormGroup;
  bookingsModelObj : Bookings=new Bookings();
  bookingsData !: any;
  showAdd!:boolean;
  showUpdate!:boolean;

  constructor(private formbuilder:FormBuilder, private router: Router, private sharedService: SharedService) { }

  ngOnInit(): void {
    this.formValue=this.formbuilder.group({
      
      BookingId : [''],
      FlightNo : [''],
      CustomerId : [''],
      Seats : [''],
      Arrival : [''],
      Departure : [''],
      Status : [''],
      BookingAmount : [''] 
    })
    this.getAllBookings();
  }
 
  getAllBookings(){
    this.sharedService.getBookings()
    .subscribe(res=>{
      this.bookingsData =res;
    })
  }
  
  deleteBookings(row:any){
    this.sharedService.deleteBookings(row.bookingID)
    .subscribe(res=>{
      alert("Reservation Cancelled Successfully!!!!!");
      this.getAllBookings();
    })
  }
  updateBookings(row:any){
    this.sharedService.updateBookings(row.bookingID)
    .subscribe(res=>{
      alert("CheckedIn Successfully!!!!!!!!!!!");
      this.getAllBookings();
    })
  }

}
