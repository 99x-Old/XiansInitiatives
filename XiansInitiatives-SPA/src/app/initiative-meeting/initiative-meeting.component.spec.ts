/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { InitiativeMeetingComponent } from './initiative-meeting.component';
import { RouterTestingModule } from '@angular/router/testing';

describe('InitiativeMeetingComponent', () => {
  let component: InitiativeMeetingComponent;
  let fixture: ComponentFixture<InitiativeMeetingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [InitiativeMeetingComponent],
      providers: [RouterTestingModule],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InitiativeMeetingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });
});
