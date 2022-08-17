import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { SharedService } from '../shared.service';

@Component({
  selector: 'app-adminprofile',
  templateUrl: './adminprofile.component.html',
  styleUrls: ['./adminprofile.component.css']
})
export class AdminprofileComponent implements OnInit {

  
  constructor(private router: Router, private jwtHelper: JwtHelperService, private sharedService: SharedService) { }
  token : any = "bearer "+ localStorage.getItem("jwt");

  decodedToken = this.jwtHelper.decodeToken(this.token);

  user:any = {
  Email: '',
  FName: '',
  LName: '',
  Role: '',
  UserId:''
  }

  public admin: any = {
    adminId: Number,
    firstName: '',
    lastName: '',
    role:'',
    username:'',
    password:'',
    emailId:''
  };

  adminId:any;

  searchadmin(id: any)
  {
    this.sharedService.getAdminById(id).subscribe(data =>
    {
      this.admin = data;
      console.log(data);
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
    if (this.user.Role != 'Admin')
    {
      this.router.navigate(["401unauthorizedaccess"]);
    }
    this.searchadmin(this.user.UserId);
  }

}
