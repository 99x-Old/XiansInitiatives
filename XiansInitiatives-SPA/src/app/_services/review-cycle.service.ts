import { ReviewCycle } from './../_models/ReviewCycle';
import { ActivatedRoute } from '@angular/router';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { ReviewCriteria } from '../_models/ReviewCriteria';

@Injectable({
  providedIn: 'root',
})
export class ReviewCycleService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getReviewCycles(initiativeId: string): Observable<ReviewCycle[]> {
    return this.http.get<Array<ReviewCycle>>(
      `${this.baseUrl}initiative/review/${initiativeId}`
    );
  }

  getEvaluationCriterions(reviewCycleId: string): Observable<ReviewCriteria[]> {
    return this.http.get<Array<ReviewCriteria>>(
      `${this.baseUrl}initiative/reviewCriteria/${reviewCycleId}`
    );
  }

  removeReviewCycle(reviewCycleId: string): Observable<ReviewCycle[]> {
    return this.http.delete<any>(
      `${this.baseUrl}initiative/review/${reviewCycleId}`
    );
  }
  removeCriteria(evaluationCriteriaId: string): Observable<ReviewCycle[]> {
    return this.http.delete<any>(
      `${this.baseUrl}initiative/reviewCriteria/${evaluationCriteriaId}`
    );
  }

  createReviewCycle(reviewCycle: ReviewCycle): Observable<any> {
    return this.http.post<ReviewCycle>(
      `${this.baseUrl}initiative/review`,
      reviewCycle
    );
  }

  createReviewCriteria(criteria: ReviewCriteria): Observable<any> {
    return this.http.post<ReviewCriteria>(
      `${this.baseUrl}initiative/reviewCriteria`,
      criteria
    );
  }
}
