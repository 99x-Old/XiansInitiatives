import { InitiativeYear } from '../_models/InitiativeYear';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { UserService } from 'src/app/_services/user.service';
import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { InitiativeService } from '../_services/initiative.service';

@Injectable()
export class YearListResolver implements Resolve<InitiativeYear[]> {
  constructor(
    private userService: UserService,
    private alertifyService: AlertifyService,
    private router: Router,
    public initiativeService: InitiativeService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<InitiativeYear[]> {
    return this.initiativeService.getInitiativeYears().pipe(
      catchError((error) => {
        this.alertifyService.error('Problem in resolving data');
        this.router.navigate(['/manageInitives']);
        return of(null);
      })
    );
  }
}
