import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AirportsComponent } from './airports/airports.component';
import { JwtModule } from "@auth0/angular-jwt";

import { HttpClientModule } from "@angular/common/http";
import { SharedService } from "./shared.service";
import { FormsModule,ReactiveFormsModule } from "@angular/forms";
import { SearchairportComponent } from './searchairport/searchairport.component';
import { AddAirportComponent } from './add-airport/add-airport.component';
import { UpdateairportComponent } from './updateairport/updateairport.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { SignupComponent } from './signup/signup.component';
import { FooterComponent } from './sharedpages/footer/footer.component';
import { HeaderComponent } from './sharedpages/header/header.component';
import { AboutusComponent } from './sharedpages/footer/aboutus/aboutus.component';
import { ContactComponent } from './sharedpages/footer/contact/contact.component';
import { CancelBookingComponent } from './cancel-booking/cancel-booking.component';
import { BookaticketComponent } from './bookaticket/bookaticket.component';
import { ChangepasswordComponent } from './changepassword/changepassword.component';
import { CustomerprofileComponent } from './customerprofile/customerprofile.component';
import { NewbookingComponent } from './newbooking/newbooking.component';
import { FlightslistComponent } from './flightslist/flightslist.component';
import { FlightsmanagementComponent } from './flightsmanagement/flightsmanagement.component';
import { AdminpasswordchangeComponent } from './adminpasswordchange/adminpasswordchange.component';
import { BookingslistComponent } from './bookingslist/bookingslist.component';
import { BookingviewComponent } from './bookingview/bookingview.component';
import { AdminComponent } from './admin/admin.component';
import { UpdateadminComponent } from './updateadmin/updateadmin.component';
import { UpdatecustomerComponent } from './updatecustomer/updatecustomer.component';
import { AdminprofileComponent } from './adminprofile/adminprofile.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { UnauthorizedAccessComponent } from './unauthorized-access/unauthorized-access.component';
import { CarouselComponent } from './carousel/carousel.component';

export function tokenGetter() { 
  return localStorage.getItem("jwt"); 
}

@NgModule({
  declarations: [
    AppComponent,
    AirportsComponent,
    SearchairportComponent,
    AddAirportComponent,
    UpdateairportComponent,
    LoginComponent,
    HomeComponent,
    SignupComponent,
    FooterComponent,
    HeaderComponent,
    AboutusComponent,
    ContactComponent,
    CancelBookingComponent,
    BookaticketComponent,
    ChangepasswordComponent,
    CustomerprofileComponent,
    NewbookingComponent,
    FlightslistComponent,
    FlightsmanagementComponent,
    AdminpasswordchangeComponent,
    BookingslistComponent,
    BookingviewComponent,
    AdminComponent,
    UpdateadminComponent,
    UpdatecustomerComponent,
    AdminprofileComponent,
    PageNotFoundComponent,
    UnauthorizedAccessComponent,
    CarouselComponent
   
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["https://localhost:7178"],
        disallowedRoutes: []
      }
    })
  ],
  providers: [SharedService, AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
