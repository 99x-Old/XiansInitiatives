import { InitiativeYear } from './../_models/InitiativeYear';
import { User } from './../_models/User';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { Initiative } from '../_models/Initiative';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  user: User;
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getUser(id: string): Observable<User> {
    return this.http.get<User>(`${this.baseUrl}user/${id}`);
  }

  getUsers(): Observable<User[]> {
    return this.http.get<Array<User>>(`${this.baseUrl}user`);
  }

  updateUser(user: User): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}user`, user);
  }

  deleteUser(id: string): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}user/${id}`);
  }

  updateProfilePicture(userId: string, formData: FormData): Observable<any> {
    return this.http.post<any>(
      `${this.baseUrl}user/upload/${userId}`,
      formData
    );
  }
}
