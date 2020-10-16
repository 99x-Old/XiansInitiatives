import { Initiative } from '../_models/Initiative';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { InitiativeService } from '../_services/initiative.service';

@Injectable()
export class InitiativeResolver implements Resolve<Initiative[]> {
  constructor(
    private alertifyService: AlertifyService,
    private router: Router,
    public initiativeService: InitiativeService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<Initiative[]> {
    return this.initiativeService.getInitiative(route.params.id).pipe(
      catchError((error) => {
        this.alertifyService.error('Problem in resolving data');
        this.router.navigate(['/updateinitiative', route.params.id]);
        return of(null);
      })
    );
  }
}
