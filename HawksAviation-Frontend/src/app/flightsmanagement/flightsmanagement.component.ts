import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Flights } from '../Models/flights.model';
import { SharedService } from '../shared.service';

@Component({
  selector: 'app-flightsmanagement',
  templateUrl: './flightsmanagement.component.html',
  styleUrls: ['./flightsmanagement.component.css']
})
export class FlightsmanagementComponent implements OnInit {

  formValue !: FormGroup;
  flightModelObj : Flights = new Flights();
  flightData !: any;
  showAdd!:boolean;
  showUpdate!:boolean;

  constructor(private formbuilder:FormBuilder,private router: Router, private jwtHelper: JwtHelperService, private sharedService: SharedService) { }

  token : any = "bearer "+ localStorage.getItem("jwt");
  decodedToken = this.jwtHelper.decodeToken(this.token);
  user:any = {
    Email: '',
    FName: '',
    LName: '',
    Role: '',
    UserId:''
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

    this.formValue=this.formbuilder.group({
      flightId : [''],
      name : [''],
      start : [''],
      destination : [''],
      arrival : ['2022-07-28T00:00:00'],
      departure : ['2022-07-28T00:00:00'],
      totalSeats : [''],
      availableSeats : [''],
      fare : [''] 
    })
    this.getAllFLights();
  }

  clickAddFlight(){
    this.formValue.reset();
    this.showAdd=true;
    this.showUpdate=false;
  }

  postFlightDetails(){
    this.flightModelObj.flightId=this.formValue.value.flightId;
    this.flightModelObj.name=this.formValue.value.name;
    this.flightModelObj.start=this.formValue.value.start;
    this.flightModelObj.destination=this.formValue.value.destination;
    this.flightModelObj.arrival=this.formValue.value.arrival;
    this.flightModelObj.departure=this.formValue.value.departure;
    this.flightModelObj.totalSeats=this.formValue.value.totalSeats;
    this.flightModelObj.availableSeats=this.formValue.value.availableSeats;
    this.flightModelObj.fare=this.formValue.value.fare;
    console.log(this.flightModelObj);
    this.sharedService.postFlight(this.flightModelObj)
      .subscribe(res=>{
      console.log(res);
      alert("Flight Data Added Successfully")
      let ref=document.getElementById('cancel')
      ref?.click();
      this.formValue.reset();
      this.getAllFLights();
    },
    err=>{
      alert("Something Went Wrong");
    })
  }

  
  getAllFLights(){
    this.sharedService.getFlight()
    .subscribe(res=>{
      this.flightData=res;
    })
  }
  deleteFlight(row:any){
    this.sharedService.deleteFlight(row.flightNo)
    .subscribe(res=>{
      alert("Flight Data Deleted");
      this.getAllFLights();
    })
  }
  onEdit(row: any){
    this.showAdd=false;
    this.showUpdate=true;
    this.flightModelObj.flightNo=row.flightNo;
    this.formValue.controls['flightId'].setValue(row.flightId);
    this.formValue.controls['name'].setValue(row.name);
    this.formValue.controls['start'].setValue(row.start);
    this.formValue.controls['destination'].setValue(row.destination);
    this.formValue.controls['arrival'].setValue(row.arrival);
    this.formValue.controls['departure'].setValue(row.departure);
    this.formValue.controls['totalSeats'].setValue(row.totalSeats);
    this.formValue.controls['availableSeats'].setValue(row.availableSeats);
    this.formValue.controls['fare'].setValue(row.fare);
  }
  updateFlight(){
    this.flightModelObj.flightId=this.formValue.value.flightId;
    this.flightModelObj.name=this.formValue.value.name;
    this.flightModelObj.start=this.formValue.value.start;
    this.flightModelObj.destination=this.formValue.value.destination;
    this.flightModelObj.arrival=this.formValue.value.arrival;
    this.flightModelObj.departure=this.formValue.value.departure;
    this.flightModelObj.totalSeats=this.formValue.value.totalSeats;
    this.flightModelObj.availableSeats=this.formValue.value.availableSeats;
    this.flightModelObj.fare=this.formValue.value.fare;
    this.sharedService.updateFlight(this.flightModelObj, this.flightModelObj.flightNo)
    .subscribe(res=>{
      alert("Updated Successfully");
      let ref=document.getElementById('cancel')
      ref?.click();
      this.formValue.reset();
      this.getAllFLights();
    })
  }

}
