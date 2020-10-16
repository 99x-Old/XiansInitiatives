import { User } from './User';

export interface Report {
  initiativeId: string;
  progress: Progress;
  contributors: Contributors;
  topContributors: User;
}

export interface Progress {
  inprogress: number;
  completed: number;
  total: number;
}

export interface Contributors {
  keyContributors: number;
  contributors: number;
}
