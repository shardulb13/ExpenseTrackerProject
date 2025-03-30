import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { TokenService } from '../services/token.service';

@Injectable({
  providedIn: 'root'
})
export class TokenauthGuard implements CanActivate {
  constructor(private tokenService: TokenService, private route: Router){}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      const token = this.tokenService.getToken();
      if(token && token != ''){
        // this.route.navigate(['user/dashboard'])
        return true;
      }
      else{
        this.route.navigate(['']);
        return false;
      }
  }
  
}
