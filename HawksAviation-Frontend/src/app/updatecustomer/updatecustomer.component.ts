import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { JwtHelperService } from '@auth0/angular-jwt';
import { SharedService } from '../shared.service';

@Component({
  selector: 'app-updatecustomer',
  templateUrl: './updatecustomer.component.html',
  styleUrls: ['./updatecustomer.component.css']
})
export class UpdatecustomerComponent implements OnInit {

  constructor(private jwtHelper: JwtHelperService, private sharedService: SharedService) { }
  
  customerForm = new FormGroup({
    customerId: new FormControl(0, [Validators.required]),
    firstName: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]),
    lastName: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]),
    username: new FormControl('', [Validators.required, Validators.minLength(6), Validators.maxLength(15)]),
    emailId: new FormControl('', [Validators.required, Validators.email, Validators.minLength(9), Validators.maxLength(25)]),
    age: new FormControl(0, [Validators.required, Validators.min(13), Validators.max(150)]),
    mobileNumber: new FormControl('', [Validators.required , Validators.minLength(10), Validators.maxLength(10)]),
    gender: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(16)])
  });

  
  public customer: any = {
    customerId : Number,
    firstName: '',
    lastName: '',
    age:'',
    username:'',
    password:'',
    emailId:'',
    gender:'',
    mobileNumber:''
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

  get f(){
    return this.customerForm.controls;
  }

  searchcustomer(id: any)
  {
    this.sharedService.getCustomerById(id).subscribe(data =>
    {
      this.customer = data;
      console.log(data);
      this.customerForm.controls.customerId.setValue(id)
      this.customerForm.controls.firstName.setValue(this.customer['firstName']);
      this.customerForm.controls.lastName.setValue(this.customer['lastName']);
      this.customerForm.controls.age.setValue(this.customer['age']);
      this.customerForm.controls.emailId.setValue(this.customer['emailId']);
      this.customerForm.controls.username.setValue(this.customer['username']);
      this.customerForm.controls.password.setValue(this.customer['password']);
      this.customerForm.controls.gender.setValue(this.customer['gender']);
      this.customerForm.controls.mobileNumber.setValue(this.customer['mobileNumber']);
      console.log(this.customerForm.value);
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
    this.searchcustomer(this.user.UserId);
  }

  Updatecustomer(id: any){
    console.log(this.customerForm.value);
    console.log(id);
    console.log(this.customer);
    this.sharedService.UpdateCustomer(id, this.customerForm.value)
                      .subscribe(data =>
                        {
                          console.log(data);
                          this.customer = data;
                          alert("Profile updated Successfully")
                          //this.router.navigate(["login"]);
                        });
  }
}
