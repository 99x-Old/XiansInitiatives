import { MeetingUser, User } from './../_models/User';
import { Component, ViewChild, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { CloudServicesConfig } from './editor/common-interfaces';
import { environment } from 'src/environments/environment';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { MeetingService } from '../_services/meeting.service';

const LOCAL_STORAGE_KEY = 'CKEDITOR_CS_CONFIG';

@Component({
  selector: 'app-root',
  templateUrl: './initiative-meeting.component.html',
  styleUrls: ['./initiative-meeting.component.scss'],
})
export class InitiativeMeetingComponent implements OnInit {
  @ViewChild('form', { static: false }) public form?: NgForm;

  public configurationSet = false;
  public newMeetingModel: any = {};
  public user: User;
  public meeting: any = {};
  public initiativeId?: string;
  public channelId = handleChannelIdInUrl();
  public config = getStoredConfig();
  public isWarning = false;
  public uploadUrl = environment.uploadUrl;
  public tokenUrl = environment.tokenUrl;
  public webSocketUrl = environment.webSocketUrl;

  public selectedUser?: string;
  constructor(
    private route: ActivatedRoute,
    private meetingService: MeetingService,
    private alertifyService: AlertifyService
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe((data) => {
      this.user = data.user;
    });
    this.initiativeId = this.route.snapshot.paramMap.get('id');
  }

  public newMeeting(): void {
    this.meeting.initiativeId = this.initiativeId;
    this.meeting.purpose = this.newMeetingModel.purpose;
    this.meeting.organizerId = localStorage.getItem('currentUser');

    storeConfig({
      tokenUrl: getRawTokenUrl(this.tokenUrl),
      uploadUrl: this.uploadUrl,
      webSocketUrl: this.webSocketUrl,
    });

    updateChannelIdInUrl(this.channelId);
    this.inviteMeetingMembers(this.channelId);
    this.selectUser(this.user);
    this.configurationSet = true;
  }
  public joinMeeting(): void {
    this.meeting.initiativeId = this.initiativeId;
    this.meeting.purpose = this.newMeetingModel.purpose;
    this.meeting.organizerId = localStorage.getItem('currentUser');

    storeConfig({
      tokenUrl: getRawTokenUrl(this.tokenUrl),
      uploadUrl: this.uploadUrl,
      webSocketUrl: this.webSocketUrl,
    });

    updateChannelIdInUrl(this.channelId);
    this.selectUser(this.user);
    this.configurationSet = true;
  }

  public inviteMeetingMembers(channelId: string): void {
    this.meetingService.inviteMeetingMembers(channelId).subscribe(
      (data: any) => {
        this.alertifyService.success('Meeting requests sent!');
      },
      (error) => {
        this.alertifyService.error('Failed to send meeting requests');
      }
    );
  }

  public selectUser(appUser: User): void {
    const user: MeetingUser = {
      id: appUser.id,
      name: appUser.fullName,
      role: 'writer',
    };

    this.selectedUser = user.id;
    this.isWarning = false;

    const keys = Object.keys(user) as (keyof MeetingUser)[];

    this.config.tokenUrl =
      `${getRawTokenUrl(this.config.tokenUrl)}?` +
      keys
        .filter((key) => user[key])
        .map((key) => {
          if (key === 'role') {
            return `${key}=${user[key]}`;
          }

          return `user.${key}=${user[key]}`;
        })
        .join('&');
  }

  public isCloudServicesTokenEndpoint(): boolean {
    return isCloudServicesTokenEndpoint(this.config.tokenUrl);
  }

  public getUserInitials(name: string): string {
    return name
      .split(' ', 2)
      .map((part) => part.charAt(0))
      .join('')
      .toUpperCase();
  }
}

function handleChannelIdInUrl(): string {
  let id = getChannelIdFromUrl();

  if (!id) {
    id = randomString();
    updateChannelIdInUrl(id);
  }

  return id;
}

function getChannelIdFromUrl(): string {
  const channelIdMatch = location.search.match(/channelId=(.+)$/);

  return channelIdMatch ? decodeURIComponent(channelIdMatch[1]) : null;
}

function randomString(): string {
  return Math.floor(Math.random() * Math.pow(2, 52)).toString(32);
}

function storeConfig(csConfig: CloudServicesConfig): void {
  localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(csConfig));
}

function updateChannelIdInUrl(id: string): any {
  window.history.replaceState({}, document.title, generateUrlWithChannelId(id));
}

function generateUrlWithChannelId(id: string): string {
  return `${window.location.href.split('?')[0]}?channelId=${id}`;
}

function getRawTokenUrl(url: string): string {
  if (isCloudServicesTokenEndpoint(url)) {
    return url.split('?')[0];
  }

  return url;
}

function isCloudServicesTokenEndpoint(tokenUrl: string): boolean {
  return /cke-cs[\w-]*\.com\/token\/dev/.test(tokenUrl);
}

function getStoredConfig(): CloudServicesConfig {
  const config = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY) || '{}');

  return {
    tokenUrl: config.tokenUrl || '',
    uploadUrl: config.uploadUrl || '',
    webSocketUrl: config.webSocketUrl || '',
  };
}
