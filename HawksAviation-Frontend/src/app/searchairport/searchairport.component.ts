import { Component, OnInit } from '@angular/core';
import { Airports } from '../Models/airports.model';
import { SharedService } from '../shared.service';

@Component({
  selector: 'app-searchairport',
  templateUrl: './searchairport.component.html',
  styleUrls: ['./searchairport.component.css']
})
export class SearchairportComponent implements OnInit {

  constructor(private sharedService: SharedService) { }

  ngOnInit(): void {
  }

  airport: any;

  searchairport(id: any)
  {
    this.sharedService.getAirportById(id).subscribe(data =>
    {
      this.airport = data;
    });
  }

}
