import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GroupsService {
  baseApiUrl = "http://localhost:46079/api/Groups"
  constructor(private httpClient: HttpClient) { }

  getGroups(): Observable<any> {
    return this.httpClient.get(`${environment.baseApiUrl}/api/Groups`);
  }

  addGroup(data: any): Observable<any> {
    return this.httpClient.post(`${environment.baseApiUrl}/api/Groups`, data);
  }

  updateGroup(data: any): Observable<any> {
    return this.httpClient.put(`${environment.baseApiUrl}/api/Groups`, data);
  }

  deleteGroup(id: number): Observable<any> {
    return this.httpClient.delete(`${environment.baseApiUrl}/api/Groups/${id}`)
  }

  deleteGroupUser(id: string): Observable<any> {
    return this.httpClient.delete(`${environment.baseApiUrl}/api/Groups/DeleteGroupUser/${id}`);
  }
}
