import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Airports } from '../Models/airports.model';
import { SharedService } from '../shared.service';

@Component({
  selector: 'app-updateairport',
  templateUrl: './updateairport.component.html',
  styleUrls: ['./updateairport.component.css']
})
export class UpdateairportComponent implements OnInit {

  constructor(private sharedService: SharedService) { }

  ngOnInit(): void {
  }

  airportForm = new FormGroup({
    airportID: new FormControl(),
    name: new FormControl(),
    city: new FormControl(),
    country: new FormControl()
  });

  
  public airport: any = {
    airportID: '',
    name: '',
    city:'',
    country:''
  };

  public airport1: any = {
    airportID: '',
    name: '',
    city:'',
    country:''
  };

  searchairport(id: any)
  {
    this.sharedService.getAirportById(id).subscribe(data =>
    {
      this.airport = data;
      this.airportForm.controls.airportID.setValue(this.airport['airportID']);
      this.airportForm.controls.name.setValue(this.airport['name']);
      this.airportForm.controls.city.setValue(this.airport['city']);
      this.airportForm.controls.country.setValue(this.airport['country']);
    });
    
  }
  
  updateAirport(id: any){
    console.log(this.airportForm.value);
    console.log(id);
    console.log(this.airport);
    this.sharedService.UpdateAirport(id, this.airportForm.value)
                      .subscribe(data =>
                        {
                          this.airport1 = data;
                          alert("Airport details updated Successfully");
                        },
                        err=>{
                          alert("Something Went Wrong" + err );
                          //this.router.navigate(["changepassword"]);
                        });
  }

}
