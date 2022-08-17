import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { JwtHelperService } from "@auth0/angular-jwt";
import { Login } from './_interfaces/login';
import{catchError, map}from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class SharedService {
  errorMsg: string = '';

  private getServerErrorMessage(error: HttpErrorResponse): string {
    switch (error.status) {
        case 404: {
            return `Not Found: ${error.message}`;
        }
        case 403: {
            return `Access Denied: ${error.message}`;
        }
        case 500: {
            return `Internal Server Error: ${error.message}`;
        }
        default: {
            return `Unknown Server Error: ${error.message}`;
        }

    }
}

  readonly APIUrl = "https://localhost:7178/api";
    constructor(private http: HttpClient) {}

    token = localStorage.getItem("jwt");
  
    headers = new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': `bearer ${this.token}`
    });
  
    requestOptions = { headers: this.headers };
    getAirportsList(): Observable <any[]> {
        return this.http.get<any>(this.APIUrl + '/airports', this.requestOptions)
        .pipe(
          catchError(error => {

              if (error.error instanceof ErrorEvent) {
                  this.errorMsg = `Error: ${error.error.message}`;
              } else {
                  this.errorMsg = this.getServerErrorMessage(error);
              }

              return throwError(this.errorMsg);
          })); 
    }

    getAdminById(id:any){
        return this.http.get<any>(this.APIUrl + '/admins/' + id, this.requestOptions).pipe(
            catchError(error => {
    
                if (error.error instanceof ErrorEvent) {
                    this.errorMsg = `Error: ${error.error.message}`;
                } else {
                    this.errorMsg = this.getServerErrorMessage(error);
                }
    
                return throwError(this.errorMsg);
            }));
    }

    UpdateAdmin(id: any, val: any){
        return this.http.put(this.APIUrl + '/admins/' + id, val, this.requestOptions).pipe(
            catchError(error => {
    
                if (error.error instanceof ErrorEvent) {
                    this.errorMsg = `Error: ${error.error.message}`;
                } else {
                    this.errorMsg = this.getServerErrorMessage(error);
                }
    
                return throwError(this.errorMsg);
            })); 
    }

    UpdateCustomer(id: any, val: any){
        return this.http.put(this.APIUrl + '/customers/' + id, val, this.requestOptions).pipe(
            catchError(error => {
    
                if (error.error instanceof ErrorEvent) {
                    this.errorMsg = `Error: ${error.error.message}`;
                } else {
                    this.errorMsg = this.getServerErrorMessage(error);
                }
    
                return throwError(this.errorMsg);
            })); 
    }

    getAirportById(id: any): Observable <any[]> {
      return this.http.get<any>(this.APIUrl + '/airports/' + id, this.requestOptions).pipe(
        catchError(error => {

            if (error.error instanceof ErrorEvent) {
                this.errorMsg = `Error: ${error.error.message}`;
            } else {
                this.errorMsg = this.getServerErrorMessage(error);
            }

            return throwError(this.errorMsg);
        }));
    }

    getCustomerById(id: any): Observable <any[]> {
      return this.http.get<any>(this.APIUrl + '/customers/' + id, this.requestOptions).pipe(
        catchError(error => {

            if (error.error instanceof ErrorEvent) {
                this.errorMsg = `Error: ${error.error.message}`;
            } else {
                this.errorMsg = this.getServerErrorMessage(error);
            }

            return throwError(this.errorMsg);
        }));
    }

    getFlightById(id: any): Observable <any[]> {
        return this.http.get<any>(this.APIUrl + '/Flights/' + id, this.requestOptions).pipe(
          catchError(error => {
  
              if (error.error instanceof ErrorEvent) {
                  this.errorMsg = `Error: ${error.error.message}`;
              } else {
                  this.errorMsg = this.getServerErrorMessage(error);
              }
  
              return throwError(this.errorMsg);
          }));
      }

    CancelBooking(id: any) {
      return this.http.delete(this.APIUrl + '/Bookings/' + id, this.requestOptions).pipe(
        catchError(error => {

            if (error.error instanceof ErrorEvent) {
                this.errorMsg = `Error: ${error.error.message}`;
            } else {
                this.errorMsg = this.getServerErrorMessage(error);
            }

            return throwError(this.errorMsg);
        }));
    }

    addAirport(val: any){
      return this.http.post(this.APIUrl + '/airports', val, this.requestOptions).pipe(
        catchError(error => {

            if (error.error instanceof ErrorEvent) {
                this.errorMsg = `Error: ${error.error.message}`;
            } else {
                this.errorMsg = this.getServerErrorMessage(error);
            }

            return throwError(this.errorMsg);
        }));
    }

    NewBooking(booking:any){
      return this.http.post(this.APIUrl + '/bookings', booking, this.requestOptions).pipe(
        catchError(error => {

            if (error.error instanceof ErrorEvent) {
                this.errorMsg = `Error: ${error.error.message}`;
            } else {
                this.errorMsg = this.getServerErrorMessage(error);
            }

            return throwError(this.errorMsg);
        }));
    }

    UpdateAirport(id: any, val: any) {
      return this.http.put(this.APIUrl + '/airports/' + id, val, this.requestOptions).pipe(
        catchError(error => {

            if (error.error instanceof ErrorEvent) {
                this.errorMsg = `Error: ${error.error.message}`;
            } else {
                this.errorMsg = this.getServerErrorMessage(error);
            }

            return throwError(this.errorMsg);
        }))
    }

    RegisterCustomer(val: any){
      return this.http.post(this.APIUrl + '/customers', val, this.requestOptions).pipe(
        catchError(error => {

            if (error.error instanceof ErrorEvent) {
                this.errorMsg = `Error: ${error.error.message}`;
            } else {
                this.errorMsg = this.getServerErrorMessage(error);
            }

            return throwError(this.errorMsg);
        }));
    }

    searchflights(from: string, To: string, doj:Date): Observable <any[]>{
      return this.http.get<any>(this.APIUrl + '/flights/'+from+'/'+To+'/'+doj, this.requestOptions).pipe(
        catchError(error => {

            if (error.error instanceof ErrorEvent) {
                this.errorMsg = `Error: ${error.error.message}`;
            } else {
                this.errorMsg = this.getServerErrorMessage(error);
            }

            return throwError(this.errorMsg);
        }));
    }

    ChangePasswordCustomer(creds: any){
      return this.http.put(this.APIUrl + '/customers', creds, this.requestOptions).pipe(
        catchError(error => {

            if (error.error instanceof ErrorEvent) {
                this.errorMsg = `Error: ${error.error.message}`;
            } else {
                this.errorMsg = this.getServerErrorMessage(error);
            }

            return throwError(this.errorMsg);
        }));
    }

    ChangePasswordAdmin(creds: any){
        return this.http.put(this.APIUrl + '/admins', creds, this.requestOptions).pipe(
          catchError(error => {
  
              if (error.error instanceof ErrorEvent) {
                  this.errorMsg = `Error: ${error.error.message}`;
              } else {
                  this.errorMsg = this.getServerErrorMessage(error);
              }
  
              return throwError(this.errorMsg);
          }));
      }

    postFlight(data:any){
      return this.http.post<any>(this.APIUrl + '/flights', data, this.requestOptions).pipe(
        catchError(error => {

            if (error.error instanceof ErrorEvent) {
                this.errorMsg = `Error: ${error.error.message}`;
            } else {
                this.errorMsg = this.getServerErrorMessage(error);
            }

            return throwError(this.errorMsg);
        }));
      
    }
    
    getFlight(){
      return this.http.get<any>(this.APIUrl + '/flights', this.requestOptions)
      .pipe(map((res:any)=>{
        return res;
      })).pipe(
        catchError(error => {

            if (error.error instanceof ErrorEvent) {
                this.errorMsg = `Error: ${error.error.message}`;
            } else {
                this.errorMsg = this.getServerErrorMessage(error);
            }

            return throwError(this.errorMsg);
        }))
    }

    updateFlight(data:any, flightNo:any){
      return this.http.put<any>(this.APIUrl + '/flights/'+flightNo, data, this.requestOptions)
      .pipe(map((res:any)=>{
        return res;
      })).pipe(
        catchError(error => {

            if (error.error instanceof ErrorEvent) {
                this.errorMsg = `Error: ${error.error.message}`;
            } else {
                this.errorMsg = this.getServerErrorMessage(error);
            }

            return throwError(this.errorMsg);
        }))
    }
    deleteFlight(flightNo : number){
      return this.http.delete<any>(this.APIUrl + '/flights/'+flightNo, this.requestOptions)
      .pipe(map((res:any)=>{
        return res;
      })).pipe(
        catchError(error => {

            if (error.error instanceof ErrorEvent) {
                this.errorMsg = `Error: ${error.error.message}`;
            } else {
                this.errorMsg = this.getServerErrorMessage(error);
            }

            return throwError(this.errorMsg);
        }))
    }
    getBookings(){
        return this.http.get<any>(this.APIUrl+ '/Bookings', this.requestOptions)
        .pipe(map((res:any)=>{
          return res;
        }))
      }

      getCustomerBookings(CID: any){
        return this.http.get<any>(this.APIUrl+ '/Bookings/custbooking/' + CID, this.requestOptions)
        .pipe(map((res:any)=>{
          return res;
        }))
      }



      deleteBookings(bookingID : number){
        return this.http.delete<any>(this.APIUrl+ '/Bookings/'+bookingID, this.requestOptions)
        .pipe(map((res:any)=>{
          return res;
        }))
      }
      updateBookings(data:any){
        return this.http.put<any>(this.APIUrl+ '/Bookings/CheckIn/'+data, this.requestOptions)
        .pipe(map((res:any)=>{
          return res;
        }))
      }

      RegisterAdmin(val: any){
        return this.http.post(this.APIUrl + '/admins', val, this.requestOptions).pipe(
            catchError(error => {
    
                if (error.error instanceof ErrorEvent) {
                    this.errorMsg = `Error: ${error.error.message}`;
                } else {
                    this.errorMsg = this.getServerErrorMessage(error);
                }
    
                return throwError(this.errorMsg);
            }));
      }

  }
