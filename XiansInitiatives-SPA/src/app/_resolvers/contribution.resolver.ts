import { ActionItemService } from '../_services/action-item.service';
import { Initiative } from '../_models/Initiative';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';

@Injectable()
export class ContributionResolver implements Resolve<Initiative[]> {
  constructor(
    private alertifyService: AlertifyService,
    private router: Router,
    private actionItemService: ActionItemService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<Initiative[]> {
    return this.actionItemService.getContribution(route.params.id).pipe(
      catchError((error) => {
        this.alertifyService.error('Problem in resolving data');
        this.router.navigate(['/updateinitiative', route.params.id]);
        return of(null);
      })
    );
  }
}
