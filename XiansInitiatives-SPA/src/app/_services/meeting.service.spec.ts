import { RouterTestingModule } from '@angular/router/testing';
/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { MeetingService } from './meeting.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('Service: Meeting', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MeetingService],
      imports: [HttpClientTestingModule, RouterTestingModule],
    });
  });

  it('should ...', inject([MeetingService], (service: MeetingService) => {
    expect(service).toBeTruthy();
  }));
});
