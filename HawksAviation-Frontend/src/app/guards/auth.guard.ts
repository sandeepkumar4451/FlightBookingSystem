import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { JwtHelperService } from "@auth0/angular-jwt";


@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  oAuthService: any;
  constructor(private router:Router, private jwtHelper: JwtHelperService){}
  
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const token = "bearer "+ localStorage.getItem("jwt");

    if (token && !this.jwtHelper.isTokenExpired(token)){
      localStorage.getItem(this.jwtHelper.decodeToken(token));
      console.log(this.jwtHelper.decodeToken(token))
      const decodedToken = this.jwtHelper.decodeToken(token)
      const givenName = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'];
      console.log(givenName);
      return true;
    }

    this.router.navigate(["401unauthorizedaccess"]);
    
      return false;
  }
  
}
