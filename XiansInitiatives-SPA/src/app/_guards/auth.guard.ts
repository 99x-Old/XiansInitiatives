import { AlertifyService } from './../_services/alertify.service';
import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from '../_services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    public authService: AuthService,
    private alertifyService: AlertifyService,
    private router: Router
  ) {}

  canActivate(next: ActivatedRouteSnapshot): boolean  {
    const roles = next.firstChild.data.roles as Array<string>;

    if (roles) {
      const match = this.authService.roleMatch(roles);
      if (match) {
        return true;
      } else {
        this.router.navigate(['/forbidden']);
        this.alertifyService.error('You are not authorized to access!');
      }
    }

    if (this.authService.loggedIn()) {
      return true;
    }

    this.alertifyService.error('You should not pass!');
    this.router.navigate(['/forbidden']);
    return false;
  }

}
