import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Airports } from '../Models/airports.model';
import { Flights } from '../Models/flights.model';
import { CustomValidators } from '../CustomValidators.validator';
import { SharedService } from '../shared.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-bookaticket',
  templateUrl: './bookaticket.component.html',
  styleUrls: ['./bookaticket.component.css']
})
export class BookaticketComponent implements OnInit {

  // SearchForm  = new FormGroup({
  //   start!: new FormControl('', [Validators.required]),
  //   destination!: new FormControl('', [Validators.required]),
  //   doj!: new FormControl('', [Validators.required]),
  // },
  // [CustomValidators.FromToValidator('start', 'destination')]);


  flightList: Flights[] = [];
  modalTitle:any;
  airports:any = {
    start!: '',
    destination!: '',
    doj: Date
  };

  isSame: boolean = false;
  validD: boolean = false;

  airportList:Airports[] = [];
  refreshairportList() {
    this.sharedService.getAirportsList().subscribe(
      data =>{
      this.airportList = data;
      console.log(data);
    }); 
  }

  constructor(private router: Router, private sharedService: SharedService) { }

  ngOnInit(): void {
    this.refreshairportList();
  }

  GetflightList(start:any, destination:any, doj:any) {

    this.airports.start = start;
    this.airports.destination= destination;
    this.airports.doj = doj;
    console.log(this.airports);
    this.router.navigateByUrl('/flightslist', { state:  this.airports});

    this.sharedService.searchflights(start, destination, doj).subscribe(data =>{
      
      this.flightList = data;
    }); 
  }

  LIsSame(F: any, T:any){
    return this.isSame = F === T;
  }

  ValidateDate(date : any){
    return this.validD = date < Date.now;
  }
 
}
