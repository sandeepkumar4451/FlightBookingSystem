import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddAirportComponent } from './add-airport/add-airport.component';
import { AdminComponent } from './admin/admin.component';
import { AdminpasswordchangeComponent } from './adminpasswordchange/adminpasswordchange.component';
import { AdminprofileComponent } from './adminprofile/adminprofile.component';
import { AirportsComponent } from './airports/airports.component';
import { BookaticketComponent } from './bookaticket/bookaticket.component';
import { BookingslistComponent } from './bookingslist/bookingslist.component';
import { BookingviewComponent } from './bookingview/bookingview.component';
import { CancelBookingComponent } from './cancel-booking/cancel-booking.component';
import { CarouselComponent } from './carousel/carousel.component';
import { ChangepasswordComponent } from './changepassword/changepassword.component';
import { CustomerprofileComponent } from './customerprofile/customerprofile.component';
import { FlightslistComponent } from './flightslist/flightslist.component';
import { FlightsmanagementComponent } from './flightsmanagement/flightsmanagement.component';
import { AuthGuard } from './guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { NewbookingComponent } from './newbooking/newbooking.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { SearchairportComponent } from './searchairport/searchairport.component';
import { AboutusComponent } from './sharedpages/footer/aboutus/aboutus.component';
import { ContactComponent } from './sharedpages/footer/contact/contact.component';
import { SignupComponent } from './signup/signup.component';
import { UnauthorizedAccessComponent } from './unauthorized-access/unauthorized-access.component';
import { UpdateadminComponent } from './updateadmin/updateadmin.component';
import { UpdateairportComponent } from './updateairport/updateairport.component';
import { UpdatecustomerComponent } from './updatecustomer/updatecustomer.component';

const routes: Routes = [
  {path: '', component:HomeComponent},
  {path: 'home', component:HomeComponent},
  {path: 'airports', component:AirportsComponent},
  {path: 'searchairport', component:SearchairportComponent},
  {path: 'addairport', component: AddAirportComponent, canActivate:[AuthGuard]},
  {path: 'editairport', component: UpdateairportComponent, canActivate:[AuthGuard]},
  {path: 'login', component: LoginComponent },
  {path: 'registeradmin', component: AdminComponent, canActivate:[AuthGuard]},
  {path: 'updateadmin', component: UpdateadminComponent, canActivate:[AuthGuard]},
  {path: 'updatecustomer', component: UpdatecustomerComponent, canActivate:[AuthGuard]},
  {path: 'contact',component:ContactComponent},
  {path: 'adminprofile', component:AdminprofileComponent, canActivate:[AuthGuard], data:{roles:'Admin'} },
  {path: 'aboutus',component:AboutusComponent},
  {path: 'bookingview', component: BookingviewComponent, canActivate:[AuthGuard]},
  {path: 'signup',component:SignupComponent},
  {path: 'carousel', component: CarouselComponent},
  {path: 'bookingslist', component:BookingslistComponent, canActivate:[AuthGuard]},
  {path: 'adminpasswordchange', component: AdminpasswordchangeComponent, canActivate:[AuthGuard]},
  {path: 'flightmanagement', component:FlightsmanagementComponent, canActivate:[AuthGuard]},
  {path: 'newbooking',component:NewbookingComponent, canActivate:[AuthGuard]},
  {path: 'cancelbooking', component: CancelBookingComponent, canActivate:[AuthGuard]},
  {path: 'bookaflight', component: BookaticketComponent},
  {path: 'flightslist', component: FlightslistComponent},
  {path: '401unauthorizedaccess', component: UnauthorizedAccessComponent},
  {path: 'customerprofile', component: CustomerprofileComponent, canActivate:[AuthGuard]},
  {path: 'changepassword', component: ChangepasswordComponent, canActivate:[AuthGuard]},
  {path: '**', component: PageNotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
