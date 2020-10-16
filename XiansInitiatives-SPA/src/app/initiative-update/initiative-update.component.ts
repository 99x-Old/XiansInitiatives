import { InitiativeMember } from './../_models/InitiativeMember';
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertifyService } from '../_services/alertify.service';
import { InitiativeService } from '../_services/initiative.service';
import { UserService } from '../_services/user.service';
import { User } from '../_models/User';
import { Initiative } from '../_models/Initiative';

@Component({
  selector: 'app-initiative-update',
  templateUrl: './initiative-update.component.html',
})
export class InitiativeUpdateComponent implements OnInit {
  updateInitiativeForm: FormGroup;
  userList: User[] = [];
  updatedInitiative: Initiative;
  selectedInitiative: Initiative;
  newMemberModel: any = {};
  initiativeMembers: User[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private alertifyService: AlertifyService,
    private userService: UserService,
    private initiativeService: InitiativeService
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe((data) => {
      this.selectedInitiative = data.initiative;
    });

    this.updateInitiativeForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: [''],
      department: ['', Validators.required],
      leadId: ['', Validators.required],
      coLeadId: ['', Validators.required],
      mentorId: ['', Validators.required],
    });

    this.getUsers();
    this.getInitiativeMembers();
  }

  onSubmit(): void {
    if (this.updateInitiativeForm.invalid) {
      return;
    }
    this.updatedInitiative = Object.assign({}, this.updateInitiativeForm.value);
    this.updatedInitiative.initiativeYearId = this.selectedInitiative.initiativeYearId;
    this.updatedInitiative.id = this.selectedInitiative.id;

    this.initiativeService.updateInitiative(this.updatedInitiative).subscribe(
      (data: any) => {
        this.alertifyService.success('Initiative is updated successfully!');
      },
      (error) => {
        this.alertifyService.error('Failed to update the record');
      }
    );
  }
  getUsers(): void {
    this.userService.getUsers().subscribe(
      (data: User[]) => {
        if (data) {
          this.userList = data;
        }
      },
      (error) => {
        this.alertifyService.error('Failed to lead all the users');
      }
    );
  }

  addInitiativeMember(user: User): void {
    if (user == null) {
      this.alertifyService.error('Please select a member');
      return;
    }

    const newMember: any = {
      initiativeId: this.selectedInitiative.id,
      memberId: user.id,
    };

    this.initiativeService.addInitiativeMember(newMember).subscribe(
      (data) => {
        this.alertifyService.success('User added successfully!');
        this.initiativeMembers.push(user);
      },
      (error) => {
        this.alertifyService.error(error.error);
      }
    );
  }

  getInitiativeMembers(): void {
    this.initiativeService
      .getInitiativeMembers(this.selectedInitiative.id)
      .subscribe(
        (data) => {
          this.initiativeMembers = data;
        },
        (error) => {
          this.alertifyService.error('Failed to add the member');
        }
      );
  }

  goBack(): void {
    this.router.navigate([
      'initiativelist',
      this.selectedInitiative.initiativeYearId,
    ]);
  }

  removeMember(memberId): void {
    this.initiativeService
      .removeInitiativeMember(this.selectedInitiative.id, memberId)
      .subscribe(
        (data) => {
          this.alertifyService.success('Member was removed successfully!');
          window.location.reload();
        },
        (error) => {
          this.alertifyService.error('Failed to remove the member');
        }
      );
  }
  goToReviewPage(): void {
    this.router.navigate(['reviewcycle', this.selectedInitiative.id]);
  }
}
