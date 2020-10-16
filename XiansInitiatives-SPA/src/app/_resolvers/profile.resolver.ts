import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { UserService } from 'src/app/_services/user.service';
import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { User } from '../_models/User';
import { AlertifyService } from '../_services/alertify.service';
import { JWTTokenService } from '../_services/JWTToken.service';
import { AuthService } from '../_services/auth.service';

@Injectable()
export class ProfileResolver implements Resolve<User> {
  constructor(
    private userService: UserService,
    private alertifyService: AlertifyService,
    private router: Router,
    public authService: AuthService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<User> {
    return this.userService.getUser(localStorage.getItem('currentUser')).pipe(
      catchError((error) => {
        this.alertifyService.error('Problem in resolving data');
        this.router.navigate(['/profile']);
        return of(null);
      })
    );
  }
}
