import { InitiativeMember } from './../_models/InitiativeMember';
import { InitiativeYear } from './../_models/InitiativeYear';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Initiative } from '../_models/Initiative';
import { User } from '../_models/User';
import { Report } from '../_models/Report';

@Injectable({
  providedIn: 'root',
})
export class InitiativeService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getInitiativeYears(): Observable<InitiativeYear[]> {
    return this.http.get<Array<InitiativeYear>>(
      `${this.baseUrl}initiative/initiativeyears`
    );
  }

  createInitiativeYear(newInitiativeYear: InitiativeYear): Observable<any> {
    return this.http.post<InitiativeYear>(
      `${this.baseUrl}initiative/year`,
      newInitiativeYear
    );
  }

  getInitiatives(yearId: string): Observable<Initiative[]> {
    return this.http.get<Array<Initiative>>(
      `${this.baseUrl}initiative/initiativeyear/${yearId}`
    );
  }

  getInitiative(id: string): Observable<Initiative[]> {
    return this.http.get<Array<Initiative>>(
      `${this.baseUrl}initiative/profile/${id}`
    );
  }

  createInitiative(newInitiative: Initiative): Observable<any> {
    return this.http.post<Initiative>(
      `${this.baseUrl}initiative/newinitiative`,
      newInitiative
    );
  }

  updateInitiative(updatedInitiative: Initiative): Observable<any> {
    return this.http.put<Initiative>(
      `${this.baseUrl}initiative/updateinitiative`,
      updatedInitiative
    );
  }

  deleteInitiative(id: string): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}initiative/${id}`);
  }

  addInitiativeMember(member: InitiativeMember): Observable<any> {
    return this.http.post<InitiativeMember>(
      `${this.baseUrl}initiativemember`,
      member
    );
  }

  getInitiativeMembers(initiativeId: string): Observable<User[]> {
    return this.http.get<Array<User>>(
      `${this.baseUrl}initiativeMember/${initiativeId}`
    );
  }

  removeInitiativeMember(initiativeId: string, memberId: string): any {
    return this.http.delete<any>(
      `${this.baseUrl}initiativeMember/${initiativeId}/${memberId}`
    );
  }

  getJoinedInitiatives(memberId: string): Observable<Initiative[]> {
    return this.http.get<Array<Initiative>>(
      `${this.baseUrl}initiativeMember/joinedInitiatives/${memberId}`
    );
  }

  getInitiativeListForCurrentYear(): Observable<Initiative[]> {
    return this.http.get<Array<Initiative>>(
      `${this.baseUrl}initiative/currentyear`
    );
  }

  checkLeadInitiatives(memberId: string): Observable<Initiative> {
    return this.http.get<Initiative>(
      `${this.baseUrl}initiative/checklead/${memberId}`
    );
  }

  getReport(initiativeId: string): Observable<Report> {
    return this.http.get<Report>(
      `${this.baseUrl}initiative/report/${initiativeId}`
    );
  }

  generateNewsLetter(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}initiative/newsLetter`);
  }

  rateMember(initiativeMember: InitiativeMember): Observable<any> {
    return this.http.post<InitiativeMember>(
      `${this.baseUrl}initiativeMember/ratemember`,
      initiativeMember
    );
  }
}
