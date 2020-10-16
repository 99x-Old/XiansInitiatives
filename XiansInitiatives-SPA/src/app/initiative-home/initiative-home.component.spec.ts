/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InitiativeHomeComponent } from './initiative-home.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ActionListComponent } from '../shared/action-list/action-list.component';

describe('InitiativeHomeComponent', () => {
  let component: InitiativeHomeComponent;
  let fixture: ComponentFixture<InitiativeHomeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [InitiativeHomeComponent, ActionListComponent],
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        ReactiveFormsModule,
        FormsModule,
        BsDatepickerModule,
      ],
      providers: [HttpClientTestingModule, ReactiveFormsModule],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InitiativeHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });
});
