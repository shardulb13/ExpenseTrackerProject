import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FriendRequestsService {
  constructor(private httpClient: HttpClient) { }

  getFriendRequests(): Observable<any> {
    return this.httpClient.get(`${environment.baseApiUrl}/api/FriendRequest`);
  }

  getFriendsData(): Observable<any> {
    return this.httpClient.get(`${environment.baseApiUrl}/api/FriendRequest/FriendsData`);
  }

  addFriendRequest(data: any): Observable<any> {
    return this.httpClient.post(`${environment.baseApiUrl}/api/FriendRequest`, data);
  }

  deleteFriend(id: number): Observable<any> {
    return this.httpClient.delete(`${environment.baseApiUrl}/api/FriendRequest/${id}`);
  }

  acceptFriendRequest(data: any): Observable<any> {
    return this.httpClient.post(`${environment.baseApiUrl}/api/FriendRequest/AcceptFriendRequest`, data);
  }
}
