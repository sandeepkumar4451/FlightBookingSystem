import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { SharedService } from '../shared.service';

@Component({
  selector: 'app-updateadmin',
  templateUrl: './updateadmin.component.html',
  styleUrls: ['./updateadmin.component.css']
})
export class UpdateadminComponent implements OnInit {

  constructor(private jwtHelper: JwtHelperService, private sharedService: SharedService) { }
  adminForm = new FormGroup({
    adminId: new FormControl(),
    firstName: new FormControl(),
    lastName: new FormControl(),
    username: new FormControl(),
    emailId: new FormControl(),
    role: new FormControl(),
    password: new FormControl()
  });

  
  public admin: any = {
    firstName: '',
    lastName: '',
    role:'',
    username:'',
    password:'',
    emailId:''
  };

  adminId:any;
  token : any = "bearer "+ localStorage.getItem("jwt");

  decodedToken = this.jwtHelper.decodeToken(this.token);

  user:any = {
  Email: '',
  FName: '',
  LName: '',
  Role: '',
  UserId:''
  }

  searchadmin(id: any)
  {
    this.sharedService.getAdminById(id).subscribe(data =>
    {
      this.admin = data;
      console.log(data);
      this.adminForm.controls.adminId.setValue(id)
      this.adminForm.controls.firstName.setValue(this.admin['firstName']);
      this.adminForm.controls.lastName.setValue(this.admin['lastName']);
      this.adminForm.controls.role.setValue(this.admin['role']);
      this.adminForm.controls.emailId.setValue(this.admin['emailId']);
      this.adminForm.controls.username.setValue(this.admin['username']);
      this.adminForm.controls.password.setValue(this.admin['password']);
      console.log(this.adminForm.value);
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
    console.log(this.user);
    this.searchadmin(this.user.UserId);
  }

  

  
  
  updateAdmin(id: any){
    console.log(this.adminForm.value);
    console.log(id);
    console.log(this.admin);
    this.sharedService.UpdateAdmin(id, this.adminForm.value)
                      .subscribe(data =>
                        {
                          console.log(data);
                          this.admin = data;
                          alert("Profile updated Successfully")
                          //this.router.navigate(["login"]);
                        });
  }

}
