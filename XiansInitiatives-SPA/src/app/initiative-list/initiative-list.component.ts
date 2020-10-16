import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AlertifyService } from '../_services/alertify.service';
import { InitiativeService } from '../_services/initiative.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Initiative } from '../_models/Initiative';
import { User } from '../_models/User';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-initiative-list',
  templateUrl: './initiative-list.component.html',
})
export class InitiativeListComponent implements OnInit {
  yearId: string;
  userList: User[] = [];
  initiatives: Initiative;
  showModal: boolean;
  showUpdateModal: boolean;
  createInitiativeForm: FormGroup;
  initiativeYears: Initiative[] = [];
  newInitiative: Initiative;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private alertifyService: AlertifyService,
    private userService: UserService,
    private initiativeService: InitiativeService
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe((data) => {
      this.initiatives = data.initiatives;
      this.yearId = this.route.snapshot.paramMap.get('id');
    });

    this.createInitiativeForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: [''],
      department: ['', Validators.required],
      leadId: ['', Validators.required],
      coLeadId: [''],
      mentorId: [''],
    });
    this.getUsers();
  }
  createInitiative(): void {
    this.showModal = true;
  }
  hidePopup(): void {
    this.showModal = false;
  }

  onSubmit(): void {
    if (this.createInitiativeForm.invalid) {
      return;
    }
    this.newInitiative = Object.assign({}, this.createInitiativeForm.value);
    this.newInitiative.initiativeYearId = this.yearId;

    this.initiativeService.createInitiative(this.newInitiative).subscribe(
      (data: any) => {
        this.alertifyService.success('Initiative is created successfully!');
        this.hidePopup();
      },
      (error) => {
        this.alertifyService.error(error.error);
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
        this.alertifyService.error('Failed to get the criterion list');
      }
    );
  }

  goToInitiative(yearId): void {
    // this.router.navigate(['/initiativelist', yearId]);
  }

  updateInitiative(id): void {
    this.router.navigate(['updateinitiative/', id]);
  }
  deleteInitiative(id): void {
    this.initiativeService.deleteInitiative(id).subscribe(
      (data: any) => {
        this.alertifyService.success('Initiative is deleted successfully!');
      },
      (error) => {
        this.alertifyService.error(error.error);
      }
    );
  }
}
