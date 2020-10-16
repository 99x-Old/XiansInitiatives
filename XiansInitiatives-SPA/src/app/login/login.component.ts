import { Component, OnInit } from '@angular/core';
import { AlertifyService } from './../_services/alertify.service';
import { AuthService } from './../_services/auth.service';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { JWTTokenService } from '../_services/JWTToken.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: [
    `
      .form-group {
        margin-top: 5%;
      }
      .card {
        margin-top: 30%;
      }
    `,
  ],
})
export class LoginComponent implements OnInit {
  model: any = {};
  jwtHelper = new JwtHelperService();
  constructor(
    public authService: AuthService,
    private alertifyService: AlertifyService,
    public tokenService: JWTTokenService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  login(): void {
    this.authService.login(this.model).subscribe(
      (data) => {
        this.alertifyService.success('Logged in successfully');
      },
      (error) => {
        this.alertifyService.error(error.error);
      },
      () => {
        this.router.navigate(['/home', this.tokenService.getUserId()]);
      }
    );
  }
}
