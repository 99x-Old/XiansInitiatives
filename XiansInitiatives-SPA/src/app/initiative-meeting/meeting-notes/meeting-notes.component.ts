import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-meeting-notes',
  templateUrl: './meeting-notes.component.html',
})
export class MeetingNotesComponent implements OnInit {
  meetingNotesHtml: string;
  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.meetingNotesHtml = this.route.snapshot.paramMap.get('note');
  }
}
