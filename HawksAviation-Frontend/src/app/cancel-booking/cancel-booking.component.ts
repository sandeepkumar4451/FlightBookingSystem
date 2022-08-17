import { Component, OnInit } from '@angular/core';
import { SharedService } from '../shared.service';
import { catchError, of } from 'rxjs';


@Component({
  selector: 'app-cancel-booking',
  templateUrl: './cancel-booking.component.html',
  styleUrls: ['./cancel-booking.component.css']
})
export class CancelBookingComponent implements OnInit {

  constructor(private sharedService: SharedService) { }

  ngOnInit(): void {
  }

  response: any;
  errors!: Error;

  CancelBooking(id: any)
  {
    of(this.sharedService.CancelBooking(id)).subscribe(data =>
    {
      this.response = data;
      console.log(data);
      alert("Cancelled Booking Successfully");
    });
  }

}
