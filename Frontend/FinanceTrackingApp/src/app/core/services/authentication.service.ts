import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { TokenService } from './token.service';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private httpClient: HttpClient, private tokenService: TokenService) { }

  regsiter(data: string): Observable<any> {
    return this.httpClient.post(`${environment.baseApiUrl}/api/Authentication/register`, data, {
      reportProgress:true,
      observe:'events'
    })
  }

  login(data: string): Observable<any> {
    return this.httpClient.post(`${environment.baseApiUrl}/api/Authentication/login`, data);
  }

  getCurrentUserDetails() {
    return this.httpClient.get(`${environment.baseApiUrl}/api/UserProfile`);
  }
  getAllUsers() {
    return this.httpClient.get(`${environment.baseApiUrl}/api/Authentication`);
  }

  UpdateUserProfile(data:string){
    return this.httpClient.put(`${environment.baseApiUrl}/api/UserProfile`, data, {
      reportProgress:true,
      observe:'events'
    });
  }
}
