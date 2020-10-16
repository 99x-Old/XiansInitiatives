/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { LeadComponent } from './lead.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { JWTTokenService } from '../_services/JWTToken.service';

describe('LeadComponent', () => {
  let component: LeadComponent;
  let fixture: ComponentFixture<LeadComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [LeadComponent],
      imports: [HttpClientTestingModule, RouterTestingModule],
      providers: [
        HttpClientTestingModule,
        { provide: JWTTokenService, useClass: MockTokenService },
      ],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LeadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

class MockTokenService {
  getUserId(): string {
    return '3ea7b5e4-71d8-40ec-ac3d-930ef74e22f0';
  }
}
