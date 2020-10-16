/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { YearListComponent } from './year-list.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';

describe('YearListComponent', () => {
  let component: YearListComponent;
  let fixture: ComponentFixture<YearListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ YearListComponent ],
      imports: [HttpClientTestingModule, RouterTestingModule, ReactiveFormsModule],
      providers: [ HttpClientTestingModule, RouterTestingModule, ReactiveFormsModule],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(YearListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
