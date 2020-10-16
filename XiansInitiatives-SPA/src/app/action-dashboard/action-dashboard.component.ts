import { Report } from './../_models/Report';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { Initiative } from '../_models/Initiative';
import { InitiativeService } from '../_services/initiative.service';
import { AlertifyService } from '../_services/alertify.service';
import { ActionItemService } from '../_services/action-item.service';
import { ActionItem } from '../_models/ActionItem';

@Component({
  selector: 'app-action-dashboard',
  templateUrl: './action-dashboard.component.html',
})
export class ActionDashboardComponent implements OnInit {
  public initiativeList: Initiative[];
  actionItems: ActionItem[] = [];
  reportData: Report;
  public chartType = 'pie';
  public firstIndex = 0;
  public progressChartDataset: Array<any> = [];
  public contributionChartDataset: Array<any> = [];

  public progressLabels: Array<any> = ['In-progress', 'Completed'];
  public contributorsLabels: Array<any> = ['Key contributors', 'Contributors'];

  public chartColors: Array<any> = [
    {
      backgroundColor: ['#5c2d91', '#FF615A'],
      hoverBackgroundColor: ['#5c2d91', '#FF615A'],
      borderWidth: 2,
    },
  ];
  columns: string[];

  public chartOptions: any = {
    responsive: true,
  };

  constructor(
    private initiativeService: InitiativeService,
    private alertifyService: AlertifyService,
    private actionItemService: ActionItemService,
    private ref: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.columns = ['item', 'description', 'start', 'deadline'];
    this.getInitiativeListForCurrentYear();
  }

  onSelectInitiative(id): void {
    this.getActionItemList(id);
    this.getReportData(id);
  }
  generateNewsLetter(): void {
    this.initiativeService.generateNewsLetter().subscribe(
      (data) => {
        this.alertifyService.success('News Letter generated successfully!');
      },
      (error) => {
        this.alertifyService.error('Failed to get the current initiative list');
      }
    );
  }
  getReportData(initiativeId: string): void {
    this.initiativeService.getReport(initiativeId).subscribe(
      (data) => {
        this.reportData = data;
        if (data != null) {
          this.progressChartDataset = [
            {
              data: [
                this.reportData.progress.inprogress,
                this.reportData.progress.completed,
              ],
              label: 'Progress dataset',
            },
          ];
          this.contributionChartDataset = [
            {
              data: [
                this.reportData.contributors.keyContributors,
                this.reportData.contributors.contributors,
              ],
              label: 'Contributor dataset',
            },
          ];
        }
      },
      (error) => {
        this.alertifyService.error('Failed to get analyzed data');
      }
    );
  }
  getInitiativeListForCurrentYear(): void {
    this.initiativeService.getInitiativeListForCurrentYear().subscribe(
      (data) => {
        this.initiativeList = data;
        this.getActionItemList(this.initiativeList[this.firstIndex].id);
        this.getReportData(this.initiativeList[this.firstIndex].id);
      },
      (error) => {
        this.alertifyService.error('Failed to get the current initiative list');
      }
    );
  }

  getActionItemList(id: string): void {
    this.actionItemService.getActionItems(id).subscribe(
      (data) => {
        this.actionItems = data;
      },
      (error) => {
        this.alertifyService.error('Failed to get the action item list');
      }
    );
  }
}
