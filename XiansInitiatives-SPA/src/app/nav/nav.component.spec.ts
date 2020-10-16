import { HasRoleDirective } from './../_directives/has-role.directive';
/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { NavComponent } from './nav.component';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('NavComponent', () => {
  let component: NavComponent;
  let fixture: ComponentFixture<NavComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [NavComponent, HasRoleDirective],
      imports: [RouterTestingModule, FormsModule, HttpClientTestingModule],
    }).compileComponents();
    const adminToken =
      'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI4YjQ5OGQ5My00ZDBlLTRkZWMtYjNkYi1lN2IwNDY0MmY0YzMiLCJ1bmlxdWVfbmFtZSI6Ik1hbGl0aFciLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE2MDE4ODI4MTUsImV4cCI6MTYwMTk2OTIxNSwiaWF0IjoxNjAxODgyODE1fQ.rPExfZ7zuVlqccG81wp1w0grJGlLOk9y97pU6mqFepVOoXdvPZvq4XMauC1KR73Uo2tNOCdS9YIQwhENRO0Dtw';
    localStorage.setItem('token', adminToken);
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NavComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
