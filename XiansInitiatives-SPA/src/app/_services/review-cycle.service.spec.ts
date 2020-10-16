/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ReviewCycleService } from './review-cycle.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('Service: ReviewCycle', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ReviewCycleService],
      imports: [ HttpClientTestingModule ]
    });
  });

  it('should ...', inject([ReviewCycleService], (service: ReviewCycleService) => {
    expect(service).toBeTruthy();
  }));
});
