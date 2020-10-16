import { ActionItem } from './../../_models/ActionItem';
import { Component, OnInit, Input } from '@angular/core';
import {
  animate,
  state,
  style,
  transition,
  trigger,
} from '@angular/animations';
import { UserComment } from 'src/app/_models/UserComment';
import { JWTTokenService } from 'src/app/_services/JWTToken.service';
import { ActionItemService } from 'src/app/_services/action-item.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-action-list',
  templateUrl: './action-list.component.html',
  styleUrls: ['./action-list.component.scss'],
  animations: [
    trigger('detailExpand', [
      state(
        'collapsed',
        style({ height: '0px', minHeight: '0', display: 'none' })
      ),
      state('expanded', style({ height: '*' })),
      transition(
        'expanded <=> collapsed',
        animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')
      ),
    ]),
  ],
})
export class ActionListComponent implements OnInit {
  @Input() columnsToDisplay;
  @Input() columnsData;
  inputModel: any = {};

  constructor(
    private tokenService: JWTTokenService,
    private actionItemService: ActionItemService,
    private alertifyService: AlertifyService
  ) {}

  ngOnInit(): void {}

  saveComment(itemId: string): void {
    const newComment: any = {
      itemId,
      commenterId: this.tokenService.getUserId(),
      comment: this.inputModel.comment,
    };
    this.actionItemService.saveComment(newComment).subscribe(
      (data: any) => {
        this.alertifyService.success('Comment added successfully!');
      },
      (error) => {
        this.alertifyService.error('Failed to save the comment');
      }
    );
  }

  updateProgress(item: ActionItem): void {
    if (this.inputModel.progress! < 0 || this.inputModel.progress! > 100) {
      this.alertifyService.warning('Progress should be between 1% - 100%');
      return;
    }

    const updatedItem: any = {
      id: item.id,
      progress: this.inputModel.progress,
      assignees: item.assignees,
    };
    this.actionItemService.updateProgress(updatedItem).subscribe(
      (data: any) => {
        this.alertifyService.success('Progress updated successfully!');
      },
      (error) => {
        this.alertifyService.error('Failed to update the progress');
      }
    );
  }
}
