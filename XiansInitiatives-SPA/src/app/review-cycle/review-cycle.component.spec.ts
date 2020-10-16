/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ReviewCycleComponent } from './review-cycle.component';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ReactiveFormsModule } from '@angular/forms';

describe('ReviewCycleComponent', () => {
  let component: ReviewCycleComponent;
  let fixture: ComponentFixture<ReviewCycleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ReviewCycleComponent],
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        ReactiveFormsModule,
      ],
      providers: [
        HttpClientTestingModule,
        RouterTestingModule,
        ReactiveFormsModule,
      ],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReviewCycleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });
});
