/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { InitiativeService } from './initiative.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('Service: Initiative', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [InitiativeService],
      imports: [ HttpClientTestingModule ]
    });
  });

  it('should ...', inject([InitiativeService], (service: InitiativeService) => {
    expect(service).toBeTruthy();
  }));
});
