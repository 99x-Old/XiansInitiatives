/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { InitiativeUpdateComponent } from './initiative-update.component';
import { FormsModule, FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { ActivatedRoute } from '@angular/router';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Observable } from 'rxjs';
import { from } from 'rxjs';

describe('InitiativeUpdateComponent', () => {
  let component: InitiativeUpdateComponent;
  let fixture: ComponentFixture<InitiativeUpdateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [InitiativeUpdateComponent],
      imports: [ReactiveFormsModule, HttpClientTestingModule],
      providers: [],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InitiativeUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });
});
