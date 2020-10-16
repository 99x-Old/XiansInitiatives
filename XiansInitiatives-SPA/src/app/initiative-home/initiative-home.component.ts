import { JWTTokenService } from './../_services/JWTToken.service';
import { Assignee } from './../_models/Assignee';
import { ActionItem } from './../_models/ActionItem';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Initiative } from '../_models/Initiative';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { InitiativeService } from '../_services/initiative.service';
import { AlertifyService } from '../_services/alertify.service';
import { User } from '../_models/User';
import { ActionItemService } from '../_services/action-item.service';
import { InitiativeMember } from '../_models/InitiativeMember';
import { Meeting } from '../_models/Meeting';
import { MeetingService } from '../_services/meeting.service';

@Component({
  selector: 'app-initiative-home',
  templateUrl: './initiative-home.component.html',
})
export class InitiativeHomeComponent implements OnInit {
  @Output() columnsToDisplay: string[];
  @Output() columnsData: any;
  columns: string[];
  initiative: Initiative;
  showModal: boolean;
  createActionItemForm: FormGroup;
  newActionItem: ActionItem;
  actionItems: ActionItem[] = [];
  initiativeUserList: User[];
  assigneesModel: any = {};
  ratingModel: any = {};
  assignees: Assignee[] = [];
  public meetings: Meeting[] = [];

  bsConfig: Partial<BsDatepickerConfig>;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder,
    private initiativeService: InitiativeService,
    private actionItemService: ActionItemService,
    private alertifyService: AlertifyService,
    private tokenServie: JWTTokenService
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe((data) => {
      this.initiative = data.initiative;
      this.actionItems = data.actionItems;
      this.meetings = data.meetings;
    });

    this.createActionItemForm = this.formBuilder.group({
      item: ['', Validators.required],
      description: ['', Validators.required],
      deadline: ['', Validators.required],
      assignees: [''],
      progress: [''],
    });
    this.columns = ['item', 'description', 'start', 'deadline'];
    this.getInitiativeMembers();
  }

  public viewMeetingNotes(meetingNote: string): void {
    this.router.navigate(['/meetingnotes', meetingNote]);
  }

  onSubmit(): void {
    if (this.createActionItemForm.invalid) {
      return;
    }
    this.newActionItem = Object.assign({}, this.createActionItemForm.value);
    this.newActionItem.assignees = this.assignees;
    this.newActionItem.initiativeId = this.initiative.id;

    this.actionItemService.createActionItem(this.newActionItem).subscribe(
      (data: any) => {
        this.alertifyService.success('Action item is created successfully!');
        this.hidePopup();
      },
      (error) => {
        this.alertifyService.error('Failed to create action item');
      }
    );
  }
  createActionItem(): void {
    this.showModal = true;
  }

  hidePopup(): void {
    this.showModal = false;
  }

  getInitiativeMembers(): void {
    this.initiativeService.getInitiativeMembers(this.initiative.id).subscribe(
      (data: any) => {
        this.initiativeUserList = data;
      },
      (error) => {
        this.alertifyService.error(error.error);
      }
    );
  }

  rateMember(model: any): void {
    if (model.memberId === undefined) {
      this.alertifyService.warning('Please select a member');
      return;
    }
    if (model.memberId === this.tokenServie.getUserId()) {
      this.alertifyService.warning('You cannot rate yourself');
      return;
    }
    if (model.rating === undefined) {
      this.alertifyService.warning('Please select a rate');
      return;
    }
    const initiativeMember: InitiativeMember = {
      initiativeId: this.initiative.id,
      memberId: model.memberId,
      rating: model.rating,
    };

    this.initiativeService.rateMember(initiativeMember).subscribe(
      (data: any) => {
        this.alertifyService.success('Rating saved successfully');
      },
      (error) => {
        this.alertifyService.error(error.error);
      }
    );
  }

  addAssignees(assigneeModel): void {
    const assignee: Assignee = {
      assigneeId: assigneeModel.id,
      fullName: assigneeModel.fullName,
    };
    this.assignees.push(assignee);
  }
  removeMember(id: string): void {
    this.assignees.pop();
  }

  goToMeetings(): void {
    this.router.navigate(['/meetings', this.initiative.id]);
  }
}
