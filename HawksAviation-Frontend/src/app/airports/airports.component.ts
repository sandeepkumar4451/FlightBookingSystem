import { Component, OnInit } from '@angular/core';
import { SharedService } from "src/app/shared.service";
import { Airports } from '../Models/airports.model';

@Component({
  selector: 'app-airports',
  templateUrl: './airports.component.html',
  styleUrls: ['./airports.component.css']
})
export class AirportsComponent implements OnInit {

  airportList:Airports[] = [];
  modalTitle:any;
  airports:any;

  constructor(private sharedService: SharedService) { }

  ngOnInit(): void {
    this.refreshairportList();
  }

  refreshairportList() {
    this.sharedService.getAirportsList().subscribe(
      data =>{
      this.airportList = data;
    }); 
  }

  
  
}
function next(next: any, arg1: (data: any) => void, arg2: (err: any) => void) {
  throw new Error('Function not implemented.');
}

