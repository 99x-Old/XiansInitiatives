import { InitiativeService } from './../_services/initiative.service';
import { InitiativeYear } from './../_models/InitiativeYear';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-year-list',
  templateUrl: './year-list.component.html',
})
export class YearListComponent implements OnInit {
  showModal: boolean;
  createInitiativeYearForm: FormGroup;
  initiativeYears: InitiativeYear[] = [];
  newInitiativeYear: InitiativeYear;

  constructor(
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private alertifyService: AlertifyService,
    private router: Router,
    private initiativeService: InitiativeService
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe((data) => {
      this.initiativeYears = data.initiativeYears;
    });

    this.createInitiativeYearForm = this.formBuilder.group({
      year: ['', Validators.required],
      description: ['', Validators.required],
    });
  }

  createInitiativeYear(): void {
    this.showModal = true;
  }
  hidePopup(): void {
    this.showModal = false;
  }

  onSubmit(): void {
    if (this.createInitiativeYearForm.invalid) {
      return;
    }
    this.newInitiativeYear = Object.assign(
      {},
      this.createInitiativeYearForm.value
    );

    this.initiativeService
      .createInitiativeYear(this.newInitiativeYear)
      .subscribe(
        (data: any) => {
          this.alertifyService.success('Initative year created!');
          this.hidePopup();
        },
        (error) => {
          this.alertifyService.error(error.error);
        }
      );
  }

  goToInitiatives(yearId): void {
    this.router.navigate(['/initiativelist', yearId]);
  }
}
