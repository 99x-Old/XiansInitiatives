import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root',
})
export class JWTTokenService {
  jwtHelper = new JwtHelperService();

  constructor() {}
  setJwtToken(token): void {
    localStorage.setItem('token', token);
  }
  getJwtToken(): string {
    return localStorage.getItem('token');
  }

  getUsername(): string {
    if (this.isTokenExpired()) {
      const decodedToken = this.jwtHelper.decodeToken(this.getJwtToken());
      return decodedToken.unique_name;
    }
  }

  getUserId(): string {
    if (this.isTokenExpired()) {
      const decodedToken = this.jwtHelper.decodeToken(this.getJwtToken());
      return decodedToken.nameid;
    }
  }

  getUserRoles(): string {
    if (this.isTokenExpired()) {
      const decodedToken = this.jwtHelper.decodeToken(this.getJwtToken());

      return decodedToken.role;
    }
  }

  isTokenExpired(): boolean {
    return !this.jwtHelper.isTokenExpired(this.getJwtToken());
  }

  removeJwtToken(): void {
    localStorage.removeItem('token');
  }
}
