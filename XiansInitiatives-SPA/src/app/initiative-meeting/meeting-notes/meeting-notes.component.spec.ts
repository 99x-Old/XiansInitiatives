/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { MeetingNotesComponent } from './meeting-notes.component';

describe('MeetingNotesComponent', () => {
  let component: MeetingNotesComponent;
  let fixture: ComponentFixture<MeetingNotesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [MeetingNotesComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MeetingNotesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });
});
