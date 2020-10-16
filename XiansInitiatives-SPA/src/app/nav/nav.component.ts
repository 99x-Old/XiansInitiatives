import { JWTTokenService } from './../_services/JWTToken.service';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styles: [
    `
      .dropdown {
        cursor: pointer;
      }
    `,
  ],
})
export class NavComponent implements OnInit {

  userId: string;
  constructor(
    public authService: AuthService,
    public tokenService: JWTTokenService,
    private alertifyService: AlertifyService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.userId = this.tokenService.getUserId();
  }

  loggedIn(): boolean {
    return this.authService.loggedIn();
  }

  getUsername(): string {
    return localStorage.getItem('username');
  }

  logout(): void {
    this.tokenService.removeJwtToken();
    this.alertifyService.message('Logged out');
    this.router.navigate(['/login']);
  }
}
