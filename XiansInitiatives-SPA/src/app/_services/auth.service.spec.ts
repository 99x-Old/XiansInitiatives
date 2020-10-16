/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { environment } from 'src/environments/environment';
import { AuthService } from './auth.service';
import {
  HttpClientTestingModule,
  HttpTestingController,
} from '@angular/common/http/testing';

describe('Service: Auth', () => {
  let service: AuthService;
  let httpMock: HttpTestingController;
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [AuthService],
    });
    httpMock = TestBed.inject(HttpTestingController);
    service = TestBed.inject(AuthService);
  });

  afterEach(() => {
    service = null;
    httpMock = null;
  });

  it('register should return Observable with right data', () => {
    // tslint:disable-next-line:prefer-const
    const postItem = {
      username: 'TestAdmin1',
      password: 'testadmin',
      role: 'Admin',
    };

    const baseUrl = environment.apiUrl + 'auth/register';
    service.register(postItem).subscribe((data: any) => {});
    const req = httpMock.expectOne(baseUrl);
    expect(req.request.method).toBe('POST');
    req.flush(postItem);
    httpMock.verify();
  });
});
