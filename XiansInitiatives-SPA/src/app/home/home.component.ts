import { Component, OnInit, Output } from '@angular/core';
import { User } from '../_models/User';
import { ActivatedRoute, Router } from '@angular/router';
import { Initiative } from '../_models/Initiative';
import { InitiativeService } from '../_services/initiative.service';
import { AlertifyService } from '../_services/alertify.service';
import { InitiativeMember } from '../_models/InitiativeMember';
import { ActionItem } from '../_models/ActionItem';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  user: User;
  joinedInitiatives: Initiative[] = [];
  initiativeList: Initiative[] = [];
  currentInitiativeYear: number;
  showModal: boolean;
  joinModel: any = {};

  @Output() columnsToDisplay: string[];
  @Output() columnsData: any;
  columns: string[];
  actionItems: ActionItem[] = [];

  constructor(
    private route: ActivatedRoute,
    private initiativeService: InitiativeService,
    private alertifyService: AlertifyService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe((data) => {
      this.user = data.user;
      this.actionItems = data.actionItems;
    });

    this.columns = ['item', 'description', 'start', 'deadline', 'progress'];
    this.currentInitiativeYear = new Date().getFullYear();
    this.getJoinedInitiatives();
    this.getInitiativeListForCurrentYear();
  }

  getJoinedInitiatives(): void {
    this.initiativeService.getJoinedInitiatives(this.user.id).subscribe(
      (data) => {
        this.joinedInitiatives = data;
      },
      (error) => {
        this.alertifyService.error('Failed to get the initiative list');
      }
    );
  }

  getInitiativeListForCurrentYear(): void {
    this.initiativeService.getInitiativeListForCurrentYear().subscribe(
      (data) => {
        this.initiativeList = data;
      },
      (error) => {
        this.alertifyService.error('Failed to get the current initiative list');
      }
    );
  }

  joinInitiative(): void {
    this.showModal = true;
  }

  viewInitiative(initiaitiveId: string): void {
    this.router.navigate(['/initiativehome', initiaitiveId]);
  }

  addInitiativeMember(newMember: InitiativeMember): void {
    if (newMember.initiativeId == null) {
      this.alertifyService.error('Please select a initiative');
      return;
    }
    newMember.memberId = this.user.id;

    this.initiativeService.addInitiativeMember(newMember).subscribe(
      (data) => {
        this.alertifyService.success('You are joined successfully!');
        window.location.reload();
      },
      (error) => {
        this.alertifyService.error('Failed to the initiative');
      }
    );
  }

  hidePopup(): void {
    this.showModal = false;
  }
}
