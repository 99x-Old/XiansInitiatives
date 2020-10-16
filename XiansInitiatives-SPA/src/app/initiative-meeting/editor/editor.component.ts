import {
  AfterViewInit,
  OnDestroy,
  Component,
  ElementRef,
  EventEmitter,
  Input,
  Output,
  ViewChild,
} from '@angular/core';
import { CKEditor5 } from '@ckeditor/ckeditor5-angular';
import * as ClassicEditorBuild from '../../../../vendor/ckeditor5/build/classic-editor-with-real-time-collaboration.js';
import { CloudServicesConfig } from './common-interfaces';
import { Meeting } from 'src/app/_models/Meeting.js';
import { Router } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service.js';
import { MeetingService } from 'src/app/_services/meeting.service.js';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.css'],
})
export class EditorComponent implements AfterViewInit, OnDestroy {
  @Input() public configuration!: CloudServicesConfig;
  @Input() public channelId!: string;
  @Input() public meeting: Meeting;
  @Output() public ready = new EventEmitter<CKEditor5.Editor>();
  @ViewChild('sidebar', { static: true }) private sidebarContainer?: ElementRef<
    HTMLDivElement
  >;
  @ViewChild('presenceList', { static: true })
  private presenceListContainer?: ElementRef<HTMLDivElement>;

  public Editor = ClassicEditorBuild;
  public editor?: CKEditor5.Editor;
  public sharableUrl: string;

  public data = this.getInitialData();

  constructor(
    private meetingService: MeetingService,
    private alertifyService: AlertifyService,
    private router: Router
  ) {}

  public get editorConfig(): any {
    return {
      cloudServices: {
        ...this.configuration,
      },
      collaboration: {
        channelId: this.channelId,
      },
      sidebar: {
        container: this.sidebar,
      },
      presenceList: {
        container: this.presenceList,
      },
    };
  }

  private sidebar = document.createElement('div');
  private presenceList = document.createElement('div');

  private boundRefreshDisplayMode = this.refreshDisplayMode.bind(this);
  private boundCheckPendingActions = this.checkPendingActions.bind(this);

  public ngAfterViewInit(): void {
    if (!this.sidebarContainer || !this.presenceListContainer) {
      throw new Error(
        'Div containers for sidebar or presence list were not found'
      );
    }

    this.sidebarContainer.nativeElement.appendChild(this.sidebar);
    this.presenceListContainer.nativeElement.appendChild(this.presenceList);
  }

  public ngOnDestroy(): void {
    window.removeEventListener('resize', this.boundRefreshDisplayMode);
    window.removeEventListener('beforeunload', this.boundCheckPendingActions);
  }

  public onReady(editor: CKEditor5.Editor): void {
    this.editor = editor;
    this.ready.emit(editor);

    // Prevent closing the tab when any action is pending.
    window.addEventListener('beforeunload', this.boundCheckPendingActions);

    // Switch between inline and sidebar annotations according to the window size.
    window.addEventListener('resize', this.boundRefreshDisplayMode);
    this.refreshDisplayMode();
    this.sharableUrl = `${environment.clientUrl}meetings?channelId=${this.channelId}`;
  }

  private checkPendingActions(domEvt): void {
    if (this.editor.plugins.get('PendingActions').hasAny) {
      domEvt.preventDefault();
      domEvt.returnValue = true;
    }
  }

  private refreshDisplayMode(): void {
    const annotations = this.editor.plugins.get('Annotations');
    const sidebarElement = this.sidebarContainer.nativeElement;

    if (window.innerWidth < 1070) {
      sidebarElement.classList.remove('narrow');
      sidebarElement.classList.add('hidden');
      annotations.switchTo('inline');
    } else if (window.innerWidth < 1300) {
      sidebarElement.classList.remove('hidden');
      sidebarElement.classList.add('narrow');
      annotations.switchTo('narrowSidebar');
    } else {
      sidebarElement.classList.remove('hidden', 'narrow');
      annotations.switchTo('wideSidebar');
    }
  }

  public saveData(): void {
    this.meeting.meetingNotes = this.editor.getData();

    this.meetingService.saveMeeting(this.meeting).subscribe(
      (data) => {
        this.alertifyService.success('Meeting notes saved successfully!');
        this.router.navigate(['/leadinitiative']);
      },
      (error) => {
        this.alertifyService.error('Failed to save the meeting notes');
      }
    );
  }
  private getInitialData(): string {
    return `
<figure class="table"><table><tbody><tr><td colspan="9"><figure class="image">
<img src="https://74902.cdn.cke-cs.com/v0rVMHVN4GODsjvszr1U/images/121c1fe6624651f2b5fb059580e358f748c5b8f775e65627.jpeg" srcset="https://74902.cdn.cke-cs.com/v0rVMHVN4GODsjvszr1U/images/121c1fe6624651f2b5fb059580e358f748c5b8f775e65627.jpeg/w_100 100w, https://74902.cdn.cke-cs.com/v0rVMHVN4GODsjvszr1U/images/121c1fe6624651f2b5fb059580e358f748c5b8f775e65627.jpeg/w_200 200w, https://74902.cdn.cke-cs.com/v0rVMHVN4GODsjvszr1U/images/121c1fe6624651f2b5fb059580e358f748c5b8f775e65627.jpeg/w_300 300w, https://74902.cdn.cke-cs.com/v0rVMHVN4GODsjvszr1U/images/121c1fe6624651f2b5fb059580e358f748c5b8f775e65627.jpeg/w_400 400w,
 https://74902.cdn.cke-cs.com/v0rVMHVN4GODsjvszr1U/images/121c1fe6624651f2b5fb059580e358f748c5b8f775e65627.jpeg/w_500 500w, https://74902.cdn.cke-cs.com/v0rVMHVN4GODsjvszr1U/images/121c1fe6624651f2b5fb059580e358f748c5b8f775e65627.jpeg/w_600 600w, https://74902.cdn.cke-cs.com/v0rVMHVN4GODsjvszr1U/images/121c1fe6624651f2b5fb059580e358f748c5b8f775e65627.jpeg/w_700 700w, https://74902.cdn.cke-cs.com/v0rVMHVN4GODsjvszr1U/images/121c1fe6624651f2b5fb059580e358f748c5b8f775e65627.jpeg/w_800 800w, https://74902.cdn.cke-cs.com/v0rVMHVN4GODsjvszr1U/images/121c1fe6624651f2b5fb059580e358f748c5b8f775e65627.jpeg/w_900 900w, https://74902.cdn.cke-cs.com/v0rVMHVN4GODsjvszr1U/images/121c1fe6624651f2b5fb059580e358f748c5b8f775e65627.jpeg/w_950 950w" sizes="100vw" width="950"></figure><figure class="image"><img src="https://74902.cdn.cke-cs.com/v0rVMHVN4GODsjvszr1U/images/9f722fc2aaa9d3c481fbc1f4edcf2c37304ef369d90d8443.png" srcset="https://74902.cdn.cke-cs.com/v0rVMHVN4GODsjvszr1U/images/9f722fc2aaa9d3c481fbc1f4edcf2c37304ef369d90d8443.png/w_84 84w, https://74902.cdn.cke-cs.com/v0rVMHVN4GODsjvszr1U/images/9f722fc2aaa9d3c481fbc1f4edcf2c37304ef369d90d8443.png/w_164 164w, https://74902.cdn.cke-cs.com/v0rVMHVN4GODsjvszr1U/images/9f722fc2aaa9d3c481fbc1f4edcf2c37304ef369d90d8443.png/w_244 244w" sizes="100vw" width="244"></figure></td></tr><tr><td>&nbsp;</td><td colspan="2"><strong>Project Name:</strong></td><td colspan="3">Data Center Upgrade</td><td><p style="text-align:right;"><strong>Date:</strong></p></td><td><p style="text-align:center;">09/25/2020</p></td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td colspan="2"><strong>Meeting Purpose:</strong></td><td colspan="3">Initial contact with client's IT sector</td><td><p style="text-align:right;"><strong>Start:</strong></p></td><td><p style="text-align:center;">9:00 AM</p></td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td colspan="2"><strong>Place:</strong></td><td colspan="3">Conference Hall</td><td><p style="text-align:right;"><strong>End:</strong></p></td><td><p style="text-align:center;">10:00 AM</p></td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td colspan="7">Attendees</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td colspan="2">Name</td><td colspan="2">Department/Company</td><td colspan="2">Email</td><td>Phone</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td colspan="2">Rod Wood</td><td colspan="2">Development</td><td colspan="2">rod.wood@projectm.com</td><td>(555) 123 456789</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td colspan="2">Jeremy Covington</td><td colspan="2">CEO</td><td colspan="2">jeremy.covington@projectm.com</td><td>(555) 999 456789</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td colspan="2">&nbsp;</td><td colspan="2">&nbsp;</td><td colspan="2">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td colspan="2">&nbsp;</td><td colspan="2">&nbsp;</td><td colspan="2">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td colspan="2">&nbsp;</td><td colspan="2">&nbsp;</td><td colspan="2">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td colspan="2">&nbsp;</td><td colspan="2">&nbsp;</td><td colspan="2">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td colspan="7">Agenda &amp; Notes</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td colspan="4">Topic</td><td colspan="2">Owner</td><td>Time</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td colspan="4">Topic description here</td><td colspan="2">Jeremy Covington</td><td>30min</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td colspan="4">&nbsp;</td><td colspan="2">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td colspan="4">&nbsp;</td><td colspan="2">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td colspan="4">&nbsp;</td><td colspan="2">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td colspan="7">Actions</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td colspan="4">Action</td><td colspan="2">To be Taken by</td><td>Due Date</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td colspan="4">Action No.1</td><td colspan="2">Rod Wood</td><td>09/26/2020</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td colspan="4">&nbsp;</td><td colspan="2">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td colspan="4">&nbsp;</td><td colspan="2">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td colspan="4">&nbsp;</td><td colspan="2">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td colspan="7">Next Meeting</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td><strong>Date:</strong></td><td>09/28/2020</td><td><strong>Time:</strong></td><td>10:00 AM</td><td><strong>&nbsp; &nbsp;Location:</strong></td><td colspan="2">Microsoft Road</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td><strong>Objective:</strong></td><td colspan="6">Project timeline adjustment</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td><p style="text-align:right;">&nbsp;&nbsp;&nbsp;<a href="https://templatelab.com/"><strong>© TemplateLab.com</strong></a></p></td><td><p style="text-align:right;">&nbsp;</p></td></tr></tbody></table></figure><p>&nbsp;</p>
`;
  }
}
