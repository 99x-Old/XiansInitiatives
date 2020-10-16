import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { Meeting } from '../_models/Meeting';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MeetingService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient, private route: ActivatedRoute) {}

  getInitiativeMeetings(initiativeId: string): Observable<Meeting[]> {
    return this.http.get<Array<Meeting>>(
      `${this.baseUrl}initiativemeeting/${initiativeId}`
    );
  }

  saveMeeting(meeting: Meeting): Observable<any> {
    return this.http.post<Meeting>(`${this.baseUrl}initiativemeeting`, meeting);
  }

  inviteMeetingMembers(channelId: string): Observable<any> {
    return this.http.post(
      `${this.baseUrl}initiativemeeting/invite/${channelId}`,
      null
    );
  }
}
