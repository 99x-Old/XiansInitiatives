import { Initiative } from '../_models/Initiative';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { UserService } from 'src/app/_services/user.service';
import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { InitiativeService } from '../_services/initiative.service';
import { ReviewCycleService } from '../_services/review-cycle.service';

@Injectable()
export class ReviewCycleResolver implements Resolve<Initiative[]> {
  constructor(
    private alertifyService: AlertifyService,
    private router: Router,
    private reviewCycleService: ReviewCycleService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<Initiative[]> {
    return this.reviewCycleService.getReviewCycles(route.params.id).pipe(
      catchError((error) => {
        this.alertifyService.error('Problem in resolving data');
        this.router.navigate(['/initiativelist', route.params.id]);
        return of(null);
      })
    );
  }
}
