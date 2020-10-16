import { UserComment } from './UserComment';
import { Assignee } from './Assignee';

export interface ActionItem {
  initiativeId: string;
  id: string;
  item: string;
  description: string;
  start: Date;
  deadline: string;
  progress: number;
  comments: UserComment;
  assignees: Assignee[];
}


