import { UserComment } from 'src/app/_models/UserComment';
import { ActivatedRoute } from '@angular/router';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { ActionItem } from '../_models/ActionItem';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class ActionItemService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  createActionItem(actionItem: ActionItem): Observable<any> {
    return this.http.post<ActionItem>(`${this.baseUrl}actionItem`, actionItem);
  }

  getActionItems(initiativeId: string): Observable<ActionItem[]> {
    return this.http.get<Array<ActionItem>>(
      `${this.baseUrl}actionItem/${initiativeId}`
    );
  }

  getContribution(memberId: string): Observable<ActionItem[]> {
    return this.http.get<Array<ActionItem>>(
      `${this.baseUrl}actionItem/contribution/${memberId}`
    );
  }

  saveComment(userComment: UserComment): Observable<any> {
    return this.http.post<UserComment>(
      `${this.baseUrl}actionItem/addcomment`,
      userComment
    );
  }

  updateProgress(item: ActionItem): Observable<any> {
    return this.http.put<ActionItem>(`${this.baseUrl}actionItem`, item);
  }
}
