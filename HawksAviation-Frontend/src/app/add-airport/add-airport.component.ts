import { Component, OnInit } from '@angular/core';
import { Airports } from '../Models/airports.model';
import { SharedService } from '../shared.service';
import { FormGroup, FormControl, Validators } from '@angular/forms'

@Component({
  selector: 'app-add-airport',
  templateUrl: './add-airport.component.html',
  styleUrls: ['./add-airport.component.css']
})
export class AddAirportComponent implements OnInit {

  constructor(private sharedService: SharedService) { }

  ngOnInit(): void {
  }
 
airportForm = new FormGroup({
  airportId : new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(10)]),
  name : new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(20)]),
  city: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(10)]),
  country: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(10)])
});

  airport: any = null;

  get f(){
    return this.airportForm.controls;
  }

  AddAirport(){
    this.sharedService.addAirport(this.airportForm.value)
                      .subscribe(data =>
                        {
                          this.airport = data;
                        },
                        err=>{
                          alert("Something Went Wrong" + err );
                          //this.router.navigate(["changepassword"]);
                        });
  }
}
