import { ReviewCycleService } from './../_services/review-cycle.service';
import { ReviewCriteria } from './../_models/ReviewCriteria';
import { ReviewCycle } from './../_models/ReviewCycle';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { JWTTokenService } from '../_services/JWTToken.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker/bs-datepicker.config';

@Component({
  selector: 'app-review-cycle',
  templateUrl: './review-cycle.component.html',
})
export class ReviewCycleComponent implements OnInit {
  reviewCycles: ReviewCycle[] = [];
  reviewCriterions: ReviewCriteria[] = [];
  criteria: ReviewCriteria;
  initiativeId: string;
  selectedReviewCycle: ReviewCycle;
  criteriaForm: FormGroup;
  reviewCycleForm: FormGroup;
  showModal: boolean;
  bsConfig: Partial<BsDatepickerConfig>;

  constructor(
    private route: ActivatedRoute,
    private alertifyService: AlertifyService,
    private reviewCycleService: ReviewCycleService,
    private tokenServie: JWTTokenService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe((data) => {
      this.reviewCycles = data.reviewCycles;
      this.initiativeId = this.route.snapshot.paramMap.get('id');
    });

    this.criteriaForm = this.formBuilder.group({
      criteria: ['', Validators.required],
      weight: [''],
      score: ['', Validators.required],
      justification: ['', Validators.required],
    });
    this.reviewCycleForm = this.formBuilder.group({
      description: ['', Validators.required],
      date: ['', Validators.required],
    });
    this.getEvaluationCriterions(this.reviewCycles[0]);
  }
  createReviewCycle(): void {
    if (this.reviewCycleForm.invalid) {
      return;
    }
    const newReviewCycle: any = {
      initiativeId: this.initiativeId,
      evaluatorId: this.tokenServie.getUserId(),
      description: this.reviewCycleForm.controls.description.value,
      scheduledAt: this.reviewCycleForm.controls.date.value,
    };

    this.reviewCycleService.createReviewCycle(newReviewCycle).subscribe(
      (data) => {
        this.alertifyService.success('Review cycle added successfully!');
      },
      (error) => {
        this.alertifyService.error('Failed to create review cycle');
      }
    );
  }

  removeReviewCycle(reviewCycleId: string): void {
    this.reviewCycleService.removeReviewCycle(reviewCycleId).subscribe(
      (data) => {
        this.alertifyService.success('Review cycle was removed successfully!');
      },
      (error) => {
        this.alertifyService.error('Failed to remove the Review cycle');
      }
    );
  }

  getEvaluationCriterions(reviewCycle: ReviewCycle): void {
    this.selectedReviewCycle = reviewCycle;

    this.reviewCycleService.getEvaluationCriterions(reviewCycle.id).subscribe(
      (data) => {
        this.reviewCriterions = data;
      },
      (error) => {
        this.alertifyService.error('Failed to get the criterion list');
      }
    );
  }

  hidePopup(): void {
    this.showModal = false;
  }

  onSubmit(): void {
    if (this.criteriaForm.invalid) {
      return;
    }
    this.criteria = Object.assign({}, this.criteriaForm.value);
    this.criteria.reviewCycleId = this.selectedReviewCycle.id;

    this.reviewCycleService.createReviewCriteria(this.criteria).subscribe(
      (data: any) => {
        this.alertifyService.success('Initiative is created successfully!');
        this.hidePopup();
      },
      (error) => {
        this.alertifyService.error(error.error);
      }
    );
  }
  createUpdateCriteria(): void {
    this.showModal = true;
  }
  removeCriteria(evaluationCriteriaId: string): void {
    this.reviewCycleService.removeCriteria(evaluationCriteriaId).subscribe(
      (data: any) => {
        this.alertifyService.success(
          'Evaluation criteria is deleted successfully!'
        );
      },
      (error) => {
        this.alertifyService.error('Failed to delete');
      }
    );
  }
}
