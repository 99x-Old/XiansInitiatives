import { User } from './../_models/User';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { JWTTokenService } from './JWTToken.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  baseUrl = environment.apiUrl + 'auth/';
  public decodeToken: any;
  jwtHelper = new JwtHelperService();

  constructor(private http: HttpClient, public tokenService: JWTTokenService) {}

  login(model: any): Observable<any> {
    return this.http.post(this.baseUrl + 'login', model).pipe(
      map((response: any) => {
        const data = response;
        if (data != null) {
          this.tokenService.setJwtToken(data.token);
          this.setCurrentUser(data.user);
        }
      })
    );
  }

  roleMatch(allowedRoles): boolean {
    let isMatch = false;
    const userRole = this.tokenService.getUserRoles();
    allowedRoles.forEach((element) => {
      if (userRole === element) {
        isMatch = true;
        return;
      }
    });
    return isMatch;
  }

  register(model: any): Observable<any> {
    return this.http.post(this.baseUrl + 'register', model);
  }

  loggedIn(): boolean {
    return this.tokenService.isTokenExpired();
  }

  setCurrentUser(user: User): void {
    localStorage.setItem('currentUser', user.id);
    localStorage.setItem('username', user.firstName);
  }
}
