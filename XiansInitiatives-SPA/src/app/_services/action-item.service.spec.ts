/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ActionItemService } from './action-item.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('Service: ActionItem', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ActionItemService],
      imports: [ HttpClientTestingModule ]
    });
  });

  it('should ...', inject([ActionItemService], (service: ActionItemService) => {
    expect(service).toBeTruthy();
  }));
});
