/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { JWTTokenService } from './JWTToken.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('Service: JWTToken', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [JWTTokenService],
      imports: [ HttpClientTestingModule ]
    });
  });

  it('should ...', inject([JWTTokenService], (service: JWTTokenService) => {
    expect(service).toBeTruthy();
  }));
});
