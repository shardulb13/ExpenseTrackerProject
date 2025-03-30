import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FriendsService {
  baseApiUrl = "http://localhost:46079/api/Friend";
  constructor(private httpClient: HttpClient) { }

  getFriends(): Observable<any> {
    return this.httpClient.get(`${environment.baseApiUrl}/api/Friend`);
  }

  getFriendsData(): Observable<any> {
    return this.httpClient.get(`${environment.baseApiUrl}/api/Friend/FriendsData`);
  }

  addFriend(data: any): Observable<any> {
    return this.httpClient.post(`${environment.baseApiUrl}/api/Friend`, data);
  }

  deleteFriend(id: number): Observable<any> {
    return this.httpClient.delete(`${environment.baseApiUrl}/api/Friend/${id}`);
  }
}
