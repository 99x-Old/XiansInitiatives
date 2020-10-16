import { Router } from '@angular/router';
import { InitiativeYear } from './../_models/InitiativeYear';
import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { Initiative } from '../_models/Initiative';
import { AlertifyService } from '../_services/alertify.service';
import { InitiativeService } from '../_services/initiative.service';
import { JWTTokenService } from '../_services/JWTToken.service';

@Component({
  selector: 'app-lead',
  templateUrl: './lead.component.html',
})
export class LeadComponent implements OnInit {
  leadInitiative: Initiative;
  constructor(
    private initiativeService: InitiativeService,
    private tokenService: JWTTokenService,
    private alertifyService: AlertifyService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.checkLeadInitiatives();
  }

  checkLeadInitiatives(): void {
    this.initiativeService
      .checkLeadInitiatives(this.tokenService.getUserId())
      .subscribe(
        (data: any) => {
          this.leadInitiative = data;
          if (data) {
            this.router.navigate(['/initiativehome', data.id]);
          } else {
            this.alertifyService.error(
              'You have not assigned to any initiative as a lead yet'
            );
            this.router.navigate(['/home', data.id]);
          }
        },
        (error) => {
          this.alertifyService.error('Problem with fetching lead list');
        }
      );
  }
}
